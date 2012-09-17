using System;
using System.Collections.Generic;

namespace Charcoal.Core.Entities
{
    public class Story : BaseEntity
    {
        public string Description { get; set; }
        public string Title { get; set; }
        public StoryStatus Status { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastEditedOn { get; set; }
        public long ProjectId { get; set; }
        public Project Project { get; set; }
        public List<Task> Tasks { get; set; }
        public int? Estimate { get; set; }
        public StoryType StoryType { get; set; }
        public IterationType IterationType { get; set; }

        //TODO: add to the database
        public string Tag { get; set; }
        public DateTime? AcceptedOn { get; set; }
        public DateTime? TransitionedOn { get; set; }

        public Story()
        {
            Description = "";
            Tag = "";
        }
    }

    public enum StoryType { Feature=0, Bug, Chore, Release }

    public enum StoryStatus
    {
        UnScheduled=0, UnStarted, Started, Finished, Delivered, Accepted, Rejected
    }
    public enum IterationType { Icebox=0,Current, Backlog, Done }
}