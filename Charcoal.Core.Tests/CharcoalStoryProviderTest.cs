using System.Collections.Generic;
using Charcoal.Core.Entities;
using Charcoal.DataLayer;
using Moq;
using NUnit.Framework;

namespace Charcoal.Core.Tests
{
    [TestFixture]
    public class CharcoalStoryProviderTest
    {
        const long projectId = 99;

        [Test]
        public void CanAddNewStory()
        {
            var story = new Story();
            var storyRepo = new Mock<IStoryRepository>(MockBehavior.Strict);
            storyRepo.Setup(repo => repo.Save(It.Is<Story>(e => e.ProjectId == projectId)))
                      .Returns(new DatabaseOperationResponse(true));

            new CharcoalStoryProvider("stuff",storyRepo.Object, Mock.Of<ITaskRepository>()).AddNewStory(projectId, story);
            storyRepo.Verify();
        }

        [Test]
        public void CanAddNewTask()
        {
            var task = new Task();
            var taskRepo = new Mock<ITaskRepository>(MockBehavior.Strict);
            taskRepo.Setup(repo => repo.Save(task)).Returns(new DatabaseOperationResponse(true));

            new CharcoalStoryProvider("stuff", Mock.Of<IStoryRepository>(), taskRepo.Object)
                                     .AddNewTask(task);
            taskRepo.Verify();
        }

        [Test]
        public void CanUpdateTaskk()
        {
            var task = new Task();
            var taskRepo = new Mock<ITaskRepository>(MockBehavior.Strict);
            taskRepo.Setup(repo => repo.Update(task)).Returns(new DatabaseOperationResponse(true));

            Assert.IsTrue(new CharcoalStoryProvider("stuff", Mock.Of<IStoryRepository>(), taskRepo.Object)
                                     .UpdateTask(task, projectId).HasSucceeded);
            taskRepo.Verify();
        }

        [Test]
        public void CanGetTask()
        {
            var taskRepo = new Mock<ITaskRepository>(MockBehavior.Strict);
            var id = 44;
            taskRepo.Setup(repo => repo.Find(id)).Returns(new Task());

            new CharcoalStoryProvider("stuff",Mock.Of<IStoryRepository>(), taskRepo.Object)
                                     .GetTask(id);
            taskRepo.Verify();
        }

        [Test]
        public void CanRemoveTask()
        {
            var taskRepo = new Mock<ITaskRepository>(MockBehavior.Strict);
            var id = 44;
            taskRepo.Setup(repo => repo.Delete(id)).Returns(new DatabaseOperationResponse(true));

            Assert.IsTrue(new CharcoalStoryProvider("stuff",Mock.Of<IStoryRepository>(), taskRepo.Object)
                                     .RemoveTask(id));
            taskRepo.Verify();
        }

        [Test]
        public void CanAddNewStoryFails()
        {
            var story = new Story();
            var storyRepo = new Mock<IStoryRepository>(MockBehavior.Strict);
            storyRepo.Setup(repo => repo.Save(It.Is<Story>(e => e.ProjectId == projectId)))
                      .Returns(new DatabaseOperationResponse());

            Assert.IsNull(new CharcoalStoryProvider("stuff",storyRepo.Object, Mock.Of<ITaskRepository>()).AddNewStory(projectId, story));
            storyRepo.Verify();
        }

        [Test]
        public void GetStories()
        {
            var story = new Story {IterationType = IterationType.Backlog};
            var storyRepo = new Mock<IStoryRepository>(MockBehavior.Strict);
            storyRepo.Setup(repo => repo.FindAllByIterationType(projectId,(int)IterationType.Backlog ))
                      .Returns(new List<dynamic>{story});

            new CharcoalStoryProvider("stuff",storyRepo.Object, Mock.Of<ITaskRepository>()).GetStories(projectId, IterationType.Backlog);
            storyRepo.Verify();
        }

        [Test]
        public void GeAlltStories()
        {
            var story = new Story { IterationType = IterationType.Backlog , ProjectId = projectId};
            var storyRepo = new Mock<IStoryRepository>(MockBehavior.Strict);
            storyRepo.Setup(repo => repo.FindAllByProjectId(projectId))
                      .Returns(new List<dynamic> { story });

            new CharcoalStoryProvider("stuff",storyRepo.Object, Mock.Of<ITaskRepository>()).GetAllStories(projectId);
            storyRepo.Verify();
        }

        [Test]
        public void CanStartStory()
        {
            var storyRepo = new Mock<IStoryRepository>(MockBehavior.Strict);
            var storyId = 22;
            storyRepo.Setup(repo => repo.UpdateStoryStatus(storyId, (int)StoryStatus.Started))
                .Returns(new Story());

            new CharcoalStoryProvider("stuff",storyRepo.Object, Mock.Of<ITaskRepository>()).StartStory(projectId, storyId, IterationType.Current);
            storyRepo.Verify();
        }

        [Test]
        public void CanFinishStory()
        {
            var storyRepo = new Mock<IStoryRepository>(MockBehavior.Strict);
            var storyId = 22;
            storyRepo.Setup(repo => repo.UpdateStoryStatus(storyId, (int) StoryStatus.Finished))
                .Returns(new Story());

            new CharcoalStoryProvider("stuff",storyRepo.Object, Mock.Of<ITaskRepository>()).FinishStory(projectId, storyId, IterationType.Current);
            storyRepo.Verify();
        }

        [Test]
        public void CanGetStory()
        {
            var storyRepo = new Mock<IStoryRepository>(MockBehavior.Strict);
            var storyId = 22;
            storyRepo.Setup(repo => repo.Find(storyId))
                .Returns(new Story());

            new CharcoalStoryProvider("stuff",storyRepo.Object, Mock.Of<ITaskRepository>()).GetStory(projectId, storyId, IterationType.Current);
            storyRepo.Verify();
        }

        [Test]
        public void CanRemoveStory()
        {
            var storyRepo = new Mock<IStoryRepository>(MockBehavior.Strict);
            var storyId = 22;
            storyRepo.Setup(repo => repo.Delete(storyId))
                .Returns(new DatabaseOperationResponse(true));

            Assert.IsTrue(new CharcoalStoryProvider("stuff",storyRepo.Object, Mock.Of<ITaskRepository>()).RemoveStory(storyId));
            storyRepo.Verify();
        }


    }
}