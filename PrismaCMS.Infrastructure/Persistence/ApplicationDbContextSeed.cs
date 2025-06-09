using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PrismaCMS.Domain.Entities;
using PrismaCMS.Domain.Enums;
using PrismaCMS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrismaCMS.Infrastructure.Persistence
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context, ILogger<ApplicationDbContextSeed> logger)
        {
            try
            {
                // Only seed if database is empty
                if (!context.Customers.Any() && !context.Employees.Any())
                {
                    // Seed Customers
                    var customers = new List<Customer>
                    {
                        new Customer(
                            "Acme Corporation",
                            "123456789",
                            new ContactInfo("contact@acme.com", "555-1234", "123 Main St", "New York", "10001", "USA")),
                        new Customer(
                            "Globex Inc",
                            "987654321",
                            new ContactInfo("info@globex.com", "555-5678", "456 Park Ave", "Boston", "02108", "USA")),
                        new Customer(
                            "Initech",
                            "456789123",
                            new ContactInfo("contact@initech.com", "555-9012", "789 Tech Blvd", "San Francisco", "94105", "USA"))
                    };

                    await context.Customers.AddRangeAsync(customers);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Seeded {Count} customers", customers.Count);

                    // Seed Employees
                    var employees = new List<Employee>
                    {
                        new Employee("John Smith", "john.smith@prismacms.com", EmployeeRole.Partner),
                        new Employee("Jane Doe", "jane.doe@prismacms.com", EmployeeRole.SeniorAccountant),
                        new Employee("David Johnson", "david.johnson@prismacms.com", EmployeeRole.Accountant),
                        new Employee("Sarah Williams", "sarah.williams@prismacms.com", EmployeeRole.Manager)
                    };

                    await context.Employees.AddRangeAsync(employees);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Seeded {Count} employees", employees.Count);

                    // Seed Financial Statements
                    var financialStatements = new List<FinancialStatement>();
                    foreach (var customer in customers)
                    {
                        var statement2023 = new FinancialStatement(2023, customer);
                        statement2023.UpdateStatus(FinancialStatementStatus.Completed);
                        financialStatements.Add(statement2023);

                        var statement2024 = new FinancialStatement(2024, customer);
                        statement2024.UpdateStatus(FinancialStatementStatus.InProgress);
                        financialStatements.Add(statement2024);
                    }

                    await context.FinancialStatements.AddRangeAsync(financialStatements);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Seeded {Count} financial statements", financialStatements.Count);

                    // Seed Assignments
                    var assignments = new List<Assignment>();
                    var random = new Random(123); // Use fixed seed for reproducibility

                    foreach (var statement in financialStatements)
                    {
                        // Assign 1-3 employees randomly to each statement
                        var assigneeCount = random.Next(1, 4);
                        var assignees = employees.OrderBy(x => random.Next()).Take(assigneeCount).ToList();

                        foreach (var employee in assignees)
                        {
                            var allocatedHours = random.Next(20, 101);  // Allocate between 20-100 hours
                            var assignment = new Assignment(statement, employee, allocatedHours);
                            assignments.Add(assignment);
                        }
                    }

                    await context.Assignments.AddRangeAsync(assignments);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Seeded {Count} assignments", assignments.Count);

                    // Seed TimeEntries
                    var timeEntries = new List<TimeEntry>();

                    foreach (var assignment in assignments)
                    {
                        // Create 2-5 time entries per assignment
                        var entryCount = random.Next(2, 6);
                        var hoursRemaining = assignment.AllocatedHours;

                        for (int i = 0; i < entryCount && hoursRemaining > 0; i++)
                        {
                            var hoursForEntry = Math.Min(random.Next(4, 9), hoursRemaining); // 4-8 hours per entry
                            var daysAgo = random.Next(1, 31); // Entry within last 30 days

                            var timeEntry = new TimeEntry(
                                assignment,
                                DateTime.UtcNow.AddDays(-daysAgo),
                                hoursForEntry,
                                $"Worked on financial statement tasks for {assignment.FinancialStatement.Customer.Name}");

                            timeEntries.Add(timeEntry);
                            hoursRemaining -= hoursForEntry;
                        }
                    }

                    await context.TimeEntries.AddRangeAsync(timeEntries);
                    await context.SaveChangesAsync();
                    logger.LogInformation("Seeded {Count} time entries", timeEntries.Count);

                    logger.LogInformation("Database seeding completed successfully");
                }
                else
                {
                    logger.LogInformation("Database already contains data - skipping seeding");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database");
                throw;
            }
        }
    }
}