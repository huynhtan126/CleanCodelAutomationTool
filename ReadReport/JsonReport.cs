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
            public int id { get; set; }
            public string username { get; set; }
            public string name { get; set; }
            public string state { get; set; }
            public string avatar_url { get; set; }
            public string web_url { get; set; }
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
            public int id { get; set; }
            public string title { get; set; }
            public string color { get; set; }
            public string description { get; set; }
            public object group_id { get; set; }
            public int project_id { get; set; }
            public bool template { get; set; }
            public string text_color { get; set; }
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
        }

        public class Root
        {
            public int id { get; set; }
            public int iid { get; set; }
            public string description { get; set; }
            public string title { get; set; }
            public int time_estimate { get; set; }
            public int total_time_spent { get; set; }
            public object human_time_estimate { get; set; }
            public object human_total_time_spent { get; set; }
            public string state { get; set; }
            public object milestone_id { get; set; }
            public int updated_by_id { get; set; }
            public DateTime created_at { get; set; }
            public DateTime updated_at { get; set; }
            public object milestone { get; set; }
            public List<Label> labels { get; set; }
            public int lock_version { get; set; }
            public int author_id { get; set; }
            public bool confidential { get; set; }
            public object discussion_locked { get; set; }
            public List<Assignee> assignees { get; set; }
            public object due_date { get; set; }
            public int project_id { get; set; }
            public object moved_to_id { get; set; }
            public object duplicated_to_id { get; set; }
            public string web_url { get; set; }
            public CurrentUser current_user { get; set; }
            public string create_note_path { get; set; }
            public string preview_note_path { get; set; }
            public bool is_project_archived { get; set; }
            public List<object> issue_email_participants { get; set; }
            public string type { get; set; }
        }
    }
}
