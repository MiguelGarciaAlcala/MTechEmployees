using Castle.Components.DictionaryAdapter.Xml;
using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace xTests.Services
{
    public class EmployeeRepositoryTests
    {
        [Fact]
        public void ExistsEmployee_True()
        {
            var employees = new List<Employee>
            {
                new Employee
                {
                    ID = 1,
                    Name = "Mariah",
                    LastName = "Carey",
                    RFC = "MCGH6501015W7",
                    BornDate = DateTime.Parse("1965-01-01"),
                    Status = EmployeeStatus.Active
                },
                new Employee
                {
                    ID = 2,
                    Name = "Tom",
                    LastName = "Cruise",
                    RFC = "TCJF6701015S4",
                    BornDate = DateTime.Parse("1967-01-01"),
                    Status = EmployeeStatus.Inactive
                }
            }.AsQueryable();

            var mockEmployees = new Mock<DbSet<Employee>>();

            mockEmployees.As<IQueryable<Employee>>()
                .Setup(m => m.Provider)
                .Returns(employees.Provider);

            mockEmployees.As<IQueryable<Employee>>()
                .Setup(m => m.Expression)
                .Returns(employees.Expression);

            mockEmployees.As<IQueryable<Employee>>()
                .Setup(m => m.ElementType)
                .Returns(employees.ElementType);

            mockEmployees.As<IQueryable<Employee>>()
                .Setup(m => m.GetEnumerator())
                .Returns(employees.GetEnumerator());

            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var mockContext = new Mock<DatabaseContext>(options);

            mockContext
                .Setup(m => m.Employees)
                .Returns(mockEmployees.Object);

            mockContext
                .Setup(m => m.Set<Employee>())
                .Returns(mockEmployees.Object);

            var repository = new EmployeeRepository(mockContext.Object);
            Assert.True(repository.Get(1) != null);
        }

        [Fact]
        public void ExistsEmployee_False()
        {

        }

        [Fact]
        public void CreateEmployee_Success()
        {

        }

        [Fact]
        public void ExistsEmployee_Error()
        {

        }

        [Fact]
        public void UpdateEmployee_Success()
        {

        }

        [Fact]
        public void UpdateEmployee_Error()
        {

        }

        [Fact]
        public void GetEmployee_Success()
        {

        }

        [Fact]
        public void GetEmployee_Error()
        {

        }

        [Fact]
        public void FilterEmployees_Success()
        {

        }

        [Fact]
        public void DeleteEmployee_Success()
        {

        }

        [Fact]
        public void DeleteEmployee_Error()
        {

        }
    }
}
