using System;
using System.Collections.Generic;
using Charcoal.Common;
using Charcoal.Core.Entities;
using Charcoal.DataLayer;
using System.Linq;

namespace Charcoal.Core
{
    public class CharcoalProjectProvider : IProjectProvider
    {
        private readonly string m_token;
        private readonly IUserRepository m_userRepository;
        private readonly IProjectRepository m_projectRepository;

        public CharcoalProjectProvider(string token, IProjectRepository projectRepository = null, IUserRepository userRepository=null)
        {
            m_token = token;
            m_userRepository = userRepository??new UserRepository();
            m_projectRepository = projectRepository ?? new ProjectRepository();
        }

        public List<Project> GetProjectsByUser(string userName)
        {
            throw new NotImplementedException();
        }

        public List<Project> GetProjects()
        {
            var projects = m_projectRepository.GetProjectsByUseToken(m_token);
            return projects.Count > 0 ? projects.ConvertAll(e => (Project) e) : new List<Project>();
        }

        public Project GetProject(long id)
        {
            var project = m_projectRepository.Find(id) as List<dynamic>;
            return project.FirstOrDefault();
        }

        public List<User> GetAvailableUsers()
        {
            var users = m_userRepository.GetAllUsers(m_token);
            return users.Count > 0 ? users.ConvertAll(e => (User)e) : new List<User>();
        }

        public OperationResponse CreateProject(Project project)
        {
            var response= m_projectRepository.CreateProjectAssociatedWithKey(project, m_token);
            return new OperationResponse(response.HasSucceeded, response.Description);
        }
    }
}