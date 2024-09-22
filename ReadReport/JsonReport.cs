using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadReport
{
    public class JsonReport
    {
        public class Assignee
        {
            public object id { get; set; }
            public object username { get; set; }
            public object name { get; set; }
            public object state { get; set; }
            public object avatar_url { get; set; }
            public object web_url { get; set; }
        }

        public class CurrentUser
        {
            public bool can_create_note { get; set; }
            public bool can_create_confidential_note { get; set; }
            public bool can_update { get; set; }
            public bool can_set_issue_metadata { get; set; }
            public bool can_award_emoji { get; set; }
        }

        public class Label
        {
            public object id { get; set; }
            public object title { get; set; }
            public object color { get; set; }
            public object description { get; set; }
            public object group_id { get; set; }
            public object project_id { get; set; }
            public object template { get; set; }
            public object text_color { get; set; }
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
        }

        public class Root
        {
            public object id { get; set; }
            public object iid { get; set; }
            public object description { get; set; }
            public object title { get; set; }
            public object time_estimate { get; set; }
            public object total_time_spent { get; set; }
            public object human_time_estimate { get; set; }
            public object human_total_time_spent { get; set; }
            public object state { get; set; }
            public object milestone_id { get; set; }
            public object updated_by_id { get; set; }
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
            public object milestone { get; set; }
            public List<Label> labels { get; set; }
            public object lock_version { get; set; }
            public object author_id { get; set; }
            public bool confidential { get; set; }
            public object discussion_locked { get; set; }
            public List<Assignee> assignees { get; set; }
            public object due_date { get; set; }
            public object project_id { get; set; }
            public object moved_to_id { get; set; }
            public object duplicated_to_id { get; set; }
            public object web_url { get; set; }
            public CurrentUser current_user { get; set; }
            public object create_note_path { get; set; }
            public object preview_note_path { get; set; }
            public object is_project_archived { get; set; }
            public List<object> issue_email_participants { get; set; }
            public object type { get; set; }
        }
    }
}
