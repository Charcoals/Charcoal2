using System;
using System.Linq;
using System.Collections.Generic;
using Charcoal.Core.Entities;
using NUnit.Framework;
using Simple.Data;

namespace Charcoal.DataLayer.Tests
{
    [TestFixture]
    public class UserRepositoryTest
    {
        private dynamic m_database;
        private UserRepository m_repository;

        [TestFixtureSetUp]
        public void Init()
        {
            m_database = Database.OpenConnection(DatabaseHelper.GetConnectionString());
            m_repository = new UserRepository(DatabaseHelper.GetConnectionString());
        }

        [TearDown]
        public void CleanUp()
        {
            m_database.Projects.DeleteAll();
            m_database.Users.DeleteAll();
        }

        [Test]
        public void CanGetAllUsers_With_Existing_ApiKey()
        {
            var user1 = new User();
            user1.APIKey = Guid.NewGuid().ToString();
            user1.UserName = "loooooooo";
            user1.LastName = "loooe3rewrrewooooo";
            user1.FirstName = "dsfsdf";
            user1.Password = "wololo";
            user1.Privileges = Privilege.Developer | Privilege.Product;
            user1.Email = "dude@dude.org";

            DatabaseOperationResponse response = m_repository.Save(user1);
            Assert.IsTrue(response.HasSucceeded);
            var user2 = new User();
            user2.APIKey = Guid.NewGuid().ToString();
            user2.UserName = "laaaaaaaaa";
            user2.LastName = "loooe3rewrrewooooo";
            user2.FirstName = "dsfsdf";
            user2.Password = "wololo";
            user2.Privileges = Privilege.Developer | Privilege.Product;
            user2.Email = "dude@dude2.org";

             response = m_repository.Save(user2);
            Assert.IsTrue(response.HasSucceeded, response.Description);

            var users = m_repository.GetAllUsers(user2.APIKey);
            Assert.AreEqual(2, users.Count);
            Assert.IsTrue(users.Exists(e=> e.APIKey == user1.APIKey));
            Assert.IsTrue(users.Exists(e => e.APIKey == user2.APIKey));
        }

        [Test]
        public void CannotGetAllUsers_With_NonExisting_ApiKey()
        {
            var user1 = new User();
            user1.APIKey = Guid.NewGuid().ToString();
            user1.UserName = "loooooooo";
            user1.LastName = "loooe3rewrrewooooo";
            user1.FirstName = "dsfsdf";
            user1.Password = "wololo";
            user1.Privileges = Privilege.Developer | Privilege.Product;
            user1.Email = "dude@dude.org";

            var response = m_repository.Save(user1);
            Assert.IsTrue(response.HasSucceeded);

            var users = m_repository.GetAllUsers(Guid.NewGuid().ToString());
            Assert.AreEqual(0, users.Count);
        }

        [Test]
        public void CanSaveUser()
        {
            var user = new User();
            user.APIKey = Guid.NewGuid().ToString();
            user.UserName = "loooooooo";
            user.LastName = "loooe3rewrrewooooo";
            user.FirstName = "dsfsdf";
            user.Password = "wololo";
            user.Privileges = Privilege.Developer | Privilege.Product;
            user.Email = "dude@dude.org";

            DatabaseOperationResponse response = m_repository.Save(user);
            Assert.IsTrue(response.HasSucceeded);

            var users = m_database.Users.All().ToList<User>();
            Assert.AreEqual(1, users.Count);

            VerifyUserStory(user, users[0]);
        }

        [Test]
        public void CanGetAPIKey()
        {
            var user = new User();
            user.APIKey = Guid.NewGuid().ToString();
            user.UserName = "loooooooo";
            user.LastName = "loooe3rewrrewooooo";
            user.FirstName = "dsfsdf";
            user.Password = "wololo";
            user.Privileges = Privilege.Developer | Privilege.Product;
            user.Email = "dude@dude.org";

            DatabaseOperationResponse response = m_repository.Save(user);
            Assert.IsTrue(response.HasSucceeded);
            var key = m_repository.GetAPIKey(user.UserName, user.Password);
            Assert.IsNotEmpty(key);
            Assert.AreEqual(user.APIKey, key);
        }

        [Test]
        public void CanValidateUser()
        {
            var user = new User();
            user.APIKey = Guid.NewGuid().ToString();
            user.UserName = "loooooooo";
            user.LastName = "loooe3rewrrewooooo";
            user.FirstName = "dsfsdf";
            user.Password = "wololo";
            user.Privileges = Privilege.Developer | Privilege.Product;
            user.Email = "dude@dude.org";

            DatabaseOperationResponse response = m_repository.Save(user);
            Assert.IsTrue(response.HasSucceeded);

            Assert.IsTrue(m_repository.IsValid(user.UserName,user.Password));
        }

        [Test]
        public void CanUpdateExistingUser()
        {
            var user = new User();
            user.APIKey = Guid.NewGuid().ToString();
            user.UserName = "loooooooo";
            user.LastName = "loooe3rewrrewooooo";
            user.FirstName = "dsfsdf";
            user.Password = "wololo";
            user.Privileges = Privilege.Product;
            user.Email = "dude@dude.org";

            DatabaseOperationResponse response = m_repository.Save(user);
            Assert.IsTrue(response.HasSucceeded);

            User retrievedUser = m_database.Users.All().ToList<User>()[0];
            retrievedUser.LastName = "New Title";

            response = m_repository.Update(retrievedUser);
            Assert.IsTrue(response.HasSucceeded);

            var users = m_database.Users.All().ToList<User>();
            Assert.AreEqual(1, users.Count);

            VerifyUserStory(retrievedUser, users[0]);
        }

        [Test]
        public void CanDeleteExistingStory()
        {
            var user = new User();
            user.APIKey = Guid.NewGuid().ToString();
            user.UserName = "loooooooo";
            user.LastName = "loooe3rewrrewooooo";
            user.FirstName = "dsfsdf";
            user.Password = "wololo";
            user.Privileges = Privilege.Product;
            user.Email = "dude@dude.org";

            DatabaseOperationResponse response = m_repository.Save(user);
            Assert.IsTrue(response.HasSucceeded);

            User retrievedUser = m_database.Users.All().ToList<User>()[0];
            response = m_repository.Delete(retrievedUser.Id);

            Assert.IsTrue(response.HasSucceeded);

            var stories = m_database.Users.All().ToList<User>();
            Assert.AreEqual(0, stories.Count);
        }

        [Test]
        public void CanFindById()
        {
            var user = new User();
            user.APIKey = Guid.NewGuid().ToString();
            user.UserName = "loooooooo";
            user.LastName = "loooe3rewrrewooooo";
            user.FirstName = "dsfsdf";
            user.Privileges = Privilege.Product;
            user.Password = "wololo";
            user.Email = "dude@dude.org";

            DatabaseOperationResponse response = m_repository.Save(user);
            Assert.IsTrue(response.HasSucceeded);

            User retrievedUser = m_database.Users.All().ToList<User>()[0];
            m_database.Projects.Insert(Title: "wololo", Description: "blah");
            
            long projectId = m_database.Projects.All().ToList<dynamic>()[0].Id;

            m_database.UsersXProjects.Insert(UserId: retrievedUser.Id, 
                                             ProjectId: projectId);

            User foundUser = m_repository.Find(retrievedUser.Id);
            VerifyUserStory(retrievedUser, foundUser);

            Assert.NotNull(foundUser.Projects);
            Assert.AreEqual(1, foundUser.Projects.Count);
            Assert.AreEqual(projectId, foundUser.Projects[0].Id);
        }

        [Test]
        public void CanFindByEmail()
        {
            var user = new User();
            user.APIKey = Guid.NewGuid().ToString();
            user.UserName = "loooooooo";
            user.LastName = "loooe3rewrrewooooo";
            user.FirstName = "dsfsdf";
            user.Privileges = Privilege.Product;
            user.Password = "wololo";
            user.Email = "dude@dude.org";

            DatabaseOperationResponse response = m_repository.Save(user);
            Assert.IsTrue(response.HasSucceeded);


            User foundUser = m_repository.FindByEmail(user.Email);
            VerifyUserStory(user, foundUser);
        }

        [Test]
        public void CanFindByUserName()
        {
            var user = new User();
            user.APIKey = Guid.NewGuid().ToString();
            user.UserName = "loooooooo";
            user.LastName = "loooe3rewrrewooooo";
            user.FirstName = "dsfsdf";
            user.Privileges = Privilege.Product;
            user.Password = "wololo";
            user.Email = "dude@dude.org";

            DatabaseOperationResponse response = m_repository.Save(user);
            Assert.IsTrue(response.HasSucceeded);


            User foundUser = m_repository.FindByUserName(user.UserName);
            VerifyUserStory(user, foundUser);
        }

        [Test]
        public void CanFindAll()
        {
            var user = new User();
            user.APIKey = Guid.NewGuid().ToString();
            user.UserName = "loooooooo";
            user.LastName = "loooe3rewrrewooooo";
            user.FirstName = "dsfsdf";
            user.Password = "wololo";
            user.Privileges = Privilege.Product;
            user.Email = "dude@dude.org";

            DatabaseOperationResponse response = m_repository.Save(user);
            Assert.IsTrue(response.HasSucceeded);

            User retrievedUser = m_database.Users.All().ToList<User>()[0];
            m_database.Projects.Insert(Title: "wololo", Description: "blah");

            long projectId = m_database.Projects.All().ToList<Project>()[0].Id;

            m_database.UsersXProjects.Insert(UserId: retrievedUser.Id,
                                             ProjectId: projectId);

            List<User> foundUsers = m_repository.FindAll().ConvertAll(e=> (User)e);
            Assert.AreEqual(1, foundUsers.Count);

            VerifyUserStory(retrievedUser, foundUsers[0]);
            Assert.NotNull(foundUsers[0].Projects);
            Assert.AreEqual(1, foundUsers[0].Projects.Count);
            Assert.AreEqual(projectId, foundUsers[0].Projects[0].Id);
        }



        private void VerifyUserStory(User expected, User actual)
        {
            Assert.AreEqual(expected.Email, actual.Email);
            Assert.AreEqual(expected.UserName, actual.UserName);
            Assert.AreEqual(expected.FirstName, actual.FirstName);
            Assert.AreEqual(expected.LastName, actual.LastName);
            Assert.AreEqual(expected.APIKey, actual.APIKey);
            Assert.AreEqual(expected.Privileges,(Privilege) actual.Privileges);
        }
    }
}