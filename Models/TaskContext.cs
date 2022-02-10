using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Models
{
    public class TaskContext: DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options): base(options)
        {
            
        }
        public DbSet<TaskResponse> Responses { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Category>().HasData(
                new Category { CategoryId=1, CategoryName = "Home"},
                new Category { CategoryId = 2, CategoryName = "School" },
                new Category { CategoryId = 3, CategoryName = "Work" },
                new Category { CategoryId = 4, CategoryName = "Church" }
            );
            mb.Entity<TaskResponse>().HasData(
                new TaskResponse
                {
                    CategoryId = 1,
                    TaskId = 1,
                    Task = "walk dog",
                    DueDate = DateTime.Parse("01/01/2022"),
                    Quadrant = "I",
                    Completed = true
                },
                new TaskResponse
                {
                    CategoryId = 2,
                    TaskId = 2,
                    Task = "breakfast",
                    DueDate = DateTime.Parse("01/11/2022"),
                    Quadrant = "II",
                    Completed = true
                },
                new TaskResponse
                {
                    CategoryId = 3,
                    TaskId = 3,
                    Task = "Eat dinner",
                    DueDate = DateTime.Parse("01/15/2022"),
                    Quadrant = "III",
                    Completed = false
                }
            );
        }
    }
}
