﻿using ConnectAzureSQL.Models;
using ConnectAzureSQL.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConnectAzureSQL.Controllers
{
    public class CourseController : Controller
    {
        private readonly CourseService _course_service;
        private readonly IConfiguration _configuration;

        public CourseController(CourseService _svc, IConfiguration configuration)
        {
            _course_service = _svc;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            // Use the configuration class to get the connection string
            IEnumerable<Course> _course_list = _course_service.GetCourses().GetAwaiter().GetResult();
            return View(_course_list);
        }

    }
}
