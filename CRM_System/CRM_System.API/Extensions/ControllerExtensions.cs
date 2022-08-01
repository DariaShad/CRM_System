﻿using CRM_System.BusinessLayer;
using Microsoft.AspNetCore.Mvc;

namespace CRM_System.API;

public static class ControllerExtensions
{
    public static string GetUrl(this ControllerBase controller) =>
        $"{controller.Request?.Scheme}://{controller.Request?.Host.Value}{controller.Request?.Path.Value}";


    public static ClaimModel GetClaims(this ControllerBase controller)
    {
        ClaimModel claimModel = new();
        if (controller.User is not null)
        {
            var claims = controller.User.Claims.ToList();
            claimModel.Email = claims[0].Value;
            //claimModel.Role = claims[1].Value;
        }

        return claimModel;
    }
}