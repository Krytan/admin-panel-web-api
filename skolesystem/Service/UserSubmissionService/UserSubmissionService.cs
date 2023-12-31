﻿using System;
using skolesystem.DTOs.UserSubmission.Request;
using skolesystem.DTOs.UserSubmission.Response;
using skolesystem.Models;
using skolesystem.Repository;
using skolesystem.Repository.AssignmentRepository;
using skolesystem.Repository.UserSubmissionRepository;

namespace skolesystem.Service.UserSubmissionService
{
    public class UserSubmissionService : IUserSubmissionService
    {
        private readonly IUserSubmissionRepository _UserSubmissionRepository;
        private readonly IAssignmentRepository _AssignmentRepository;
        private readonly IUsersRepository _UsersRepository;


        public UserSubmissionService(IUserSubmissionRepository UserSubmissionRepository, IAssignmentRepository AssignmentRepository, IUsersRepository usersRepository)
        {
            _UserSubmissionRepository = UserSubmissionRepository;
            _AssignmentRepository = AssignmentRepository;
            _UsersRepository = usersRepository;
        }

        public async Task<List<UserSubmissionResponse>> GetAll()
        {
            // Kald repository-metode for at hente alle UserSubmissions,fra databasen
            List<UserSubmission> UserSubmission = await _UserSubmissionRepository.SelectAllUserSubmissions();
            return UserSubmission.Select(a => new UserSubmissionResponse
            {
                userSubmission_Id = a.submission_id,
                userSubmission_text = a.submission_text,
                userSubmission_date = a.submission_date,
                is_deleted = a.is_deleted,
                userSubmissionAssignmentResponse = new UserSubmissionAssignmentResponse
                {
                    opgave_Id = a.Assignment.assignment_id,
                    opgave_Description = a.Assignment.assignment_description,
                    opgave_Deadline = a.Assignment.assignment_deadline,
                    is_Deleted = a.Assignment.is_Deleted
                },
                userSubmissionUserResponse = new UserSubmissionUserResponse
                {
                    user_id = a.User.user_id,
                    surname = a.User.surname,
                    email = a.User.email
                }
            }).ToList();
        }

        public async Task<UserSubmissionResponse> GetById(int UserSubmissionId)
        {
            // Kald repository-metode for at hente et specifikt UserSubmissions,fra databasen
            UserSubmission UserSubmission = await _UserSubmissionRepository.SelectUserSubmissionById(UserSubmissionId);
            return UserSubmission == null ? null : new UserSubmissionResponse
            {
                userSubmission_Id = UserSubmission.submission_id,
                userSubmission_text = UserSubmission.submission_text,
                userSubmission_date = UserSubmission.submission_date,
                userSubmissionAssignmentResponse = new UserSubmissionAssignmentResponse
                {
                    opgave_Id = UserSubmission.Assignment.assignment_id,
                    opgave_Description = UserSubmission.Assignment.assignment_description,
                    opgave_Deadline = UserSubmission.Assignment.assignment_deadline,
                    is_Deleted = UserSubmission.Assignment.is_Deleted
                },
                userSubmissionUserResponse = new UserSubmissionUserResponse
                {
                    user_id = UserSubmission.User.user_id,
                    surname = UserSubmission.User.surname,
                    email = UserSubmission.User.email
                }
            };
        }
        public async Task<UserSubmissionResponse> Create(NewUserSubmission newUserSubmission)
        {
            // Opret et nyt UserSubmission-objekt ved hjælp af værdier fra NewUserSubmission
            UserSubmission UserSubmission = new UserSubmission
            {
                submission_text = newUserSubmission.userSubmission_text,
                submission_date = newUserSubmission.userSubmission_date,
                is_deleted = newUserSubmission.is_deleted,
                user_id = newUserSubmission.UserId,
                assignment_id = newUserSubmission.assignmentId
            };
            // Indsæt den nye UserSubmission i repository 
            UserSubmission = await _UserSubmissionRepository.InsertNewUserSubmission(UserSubmission);

            return UserSubmission == null ? null : new UserSubmissionResponse
            {
                userSubmission_text = UserSubmission.submission_text,
                userSubmission_date = UserSubmission.submission_date,
                is_deleted = UserSubmission.is_deleted,
            };
        }

        public async Task<UserSubmissionResponse> Update(int UserSubmissionId, UpdateUserSubmission updateUserSubmission)
        {
            // Opret et nyt UserSubmission-objekt med opdaterede oplysninger
            UserSubmission UserSubmission = new UserSubmission
            {
                submission_text = updateUserSubmission.userSubmission_text,
                submission_date = updateUserSubmission.userSubmission_date,
                submission_id = updateUserSubmission.submissionId
            };
            // Opdater den eksisterende UserSubmission i repository'en asynkront
            UserSubmission = await _UserSubmissionRepository.UpdateExistingUserSubmission(UserSubmissionId, UserSubmission);
            if (UserSubmission == null) return null;
            else
            {
                await _AssignmentRepository.SelectAssignmentById(UserSubmission.submission_id);
                return UserSubmission == null ? null : new UserSubmissionResponse
                {
                   
                };
            }
        }
        public async Task<bool> Delete(int UserSubmissionId)
        {
            // Anmod om sletning af UserSubmission fra repository
            var result = await _UserSubmissionRepository.DeleteUserSubmission(UserSubmissionId);
            // Returner true, hvis sletningen var vellykket
            return (result != null);
        }


    }
}
