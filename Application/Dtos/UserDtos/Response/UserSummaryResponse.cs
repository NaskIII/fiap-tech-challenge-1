﻿namespace Application.Dtos.UserDtos.Response
{
    public class UserSummaryResponse
    {

        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
