using System;
using Xunit;
using CCMS.Models;
using Microsoft.EntityFrameworkCore;
using CCMS.Data;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Getters()
        {
            //arrange
            Petition petition = new Petition();
            petition.Name = "Jason";

            //Assert
            Assert.Equal("Jason", petition.Name);
        }

        [Fact]
        public void Getter2()
        {
            //arrange
            Petition petition = new Petition();
            petition.Name = "Jason";
            petition.Orginization = "stuff";

            //Assert
            Assert.Equal("stuff", petition.Orginization);
        }

        [Fact]
        public void Setters()
        {
            //arrange
            Petition petition = new Petition();
            petition.Name = "Jason";
            petition.Orginization = "stuff";

            //act
            petition.Name = "Jen";

            //Assert
            Assert.Equal("Jen", petition.Name);
        }

        [Fact]
        public void Setters2()
        {
            //arrange
            Petition petition = new Petition();
            petition.Name = "Jason";
            petition.Orginization = "stuff";

            //act
            petition.Orginization = "ccms";

            //Assert
            Assert.Equal("ccms", petition.Orginization);
        }

        [Fact]
        public async void CreatePetitionWorks()
        {
            DbContextOptions<CCMSBuildDbContext> options =
                new DbContextOptionsBuilder<CCMSBuildDbContext>
                ().UseInMemoryDatabase("CreatePetition").Options;

            using (CCMSBuildDbContext context = new CCMSBuildDbContext(options))
            {
                // arrange
                Petition petition = new Petition();
                petition.ID = 1;
                petition.Name = "Jason";
                petition.Orginization = "stuff";

                // Act
                context.Petition.Add(petition);

                context.SaveChanges();

                var created = await context.Petition.FirstOrDefaultAsync(p => p.ID == petition.ID);

                // Assert
                Assert.Equal(petition, created);

            }
        }

        [Fact]
        public async void DeletePetitionWorks()
        {
            DbContextOptions<CCMSBuildDbContext> options =
                new DbContextOptionsBuilder<CCMSBuildDbContext>
                ().UseInMemoryDatabase("DeletePetition").Options;

            using (CCMSBuildDbContext context = new CCMSBuildDbContext(options))
            {
                // arrange
                Petition petition = new Petition();
                petition.ID = 1;
                petition.Name = "Jason";
                petition.Orginization = "stuff";

                // Act
                context.Petition.Add(petition);

                context.SaveChanges();

                var toDelete = await context.Petition.FirstOrDefaultAsync(p => p.ID == petition.ID);

                context.Petition.Remove(toDelete);

                context.SaveChanges();

                var deleted = await context.Petition.FirstOrDefaultAsync(p => p.ID == petition.ID);

                // Assert
                Assert.Null(deleted);

            }
        }

        [Fact]
        public async void EditPetitionWorks()
        {
            DbContextOptions<CCMSBuildDbContext> options =
                new DbContextOptionsBuilder<CCMSBuildDbContext>
                ().UseInMemoryDatabase("EditPetition").Options;

            using (CCMSBuildDbContext context = new CCMSBuildDbContext(options))
            {
                // arrange
                Petition petition = new Petition();
                petition.ID = 1;
                petition.Name = "Jason";
                petition.Orginization = "stuff";

                // Act
                context.Petition.Add(petition);

                context.SaveChanges();

                var created = await context.Petition.FirstOrDefaultAsync(p => p.ID == petition.ID);

                created.Name = "Jen";

                context.SaveChanges();

                // Assert
                Assert.Equal("Jen", created.Name);

            }
        }

    }
}
