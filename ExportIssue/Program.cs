using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

string token = "glpat-kvcg_foZaePzKwp8cNx7";
string baseUrl = "https://gitlab.tgl-cloud.com/api/v4/projects/PrimaSolutions%2Fnewcadgrp%2Fnewcad/issues?per_page=100";
var excludedLabels = new HashSet<string> { "_Done: Đã xong", "_RootCause" };
var labelMap = new Dictionary<string, string>
{
    { "_Todo", "Chờ làm" },
    { "_In-Progress", "Đang làm" },
    { "_Pending", "Tạm ngưng" }
};

Console.OutputEncoding = Encoding.UTF8;

var client = new HttpClient();
client.DefaultRequestHeaders.Add("PRIVATE-TOKEN", token);

var allIssues = new List<GitLabIssue>();
int page = 1;

Console.WriteLine("🔄 Đang tải dữ liệu từ GitLab...");

while (true)
{
    var response = await client.GetAsync($"{baseUrl}&page={page}");

    if (!response.IsSuccessStatusCode)
    {
        Console.WriteLine($"❌ Lỗi khi gọi API: {response.StatusCode}");
        break;
    }

    var content = await response.Content.ReadAsStringAsync();

    var issues = JsonConvert.DeserializeObject<List<GitLabIssue>>(content);
    if (issues == null || issues.Count == 0)
        break;

    allIssues.AddRange(issues);

    if (!response.Headers.TryGetValues("X-Next-Page", out var nextPageHeader) || string.IsNullOrEmpty(nextPageHeader.First()))
        break;

    page = int.Parse(nextPageHeader.First());
}

var filteredIssues = allIssues
    .Where(issue => issue.Labels != null && !issue.Labels.Any(label => excludedLabels.Contains(label)))
    .ToList();
// Gom nhóm theo assignee
var statistics = filteredIssues
    .GroupBy(i => i.Assignee?.Name ?? "Unassigned")
    .Select(g => new
    {
        Assignee = g.Key,
        Total = g.Count(),
        DoiSpec = g.Count(i => i.Labels?.Contains("_Đợi Spec") == true),
        ChoLam = g.Count(i => i.Labels?.Contains("_Todo: Chờ làm") == true),
        DangLam = g.Count(i => i.Labels?.Contains("_In-Progress: Đang làm") == true),
        TamNgung = g.Count(i => i.Labels?.Contains("_Pending:  Tạm ngưng (lý do)") == true),
        Testing = g.Count(i => i.Labels?.Contains("Testing : đã xong chức năng chờ test") == true),
        Plan10 = g.Count(i => i.Labels?.Contains("Plan 10-12") == true),
        Plan1 = g.Count(i => i.Labels?.Contains("Plan 1-3 2026") == true),
        CanNotPlan = g.Count(i => i.Labels?.Contains("Can not plan (not enough information)") == true),
        NotSeenYet = g.Count() - g.Count(i => i.Labels?.Contains("Plan 10-12") == true) - g.Count(i => i.Labels?.Contains("Plan 1-3 2026") == true) - g.Count(i => i.Labels?.Contains("Can not plan (not enough information)") == true),
        ids = string.Join("", g.Select(i => i.iid.ToString() + ";"))
    })
    .OrderByDescending(g => g.ChoLam)
    .ToList();
var filteredIssuesKH = allIssues
    .Where(issue => issue.Labels != null && !issue.Labels.Any(label => excludedLabels.Contains(label)) && issue.Labels.Any(x=>x.ToUpper().Contains("CLIENT")))
    .ToList();

Console.WriteLine($"✅ Tổng số issue: {filteredIssuesKH.Count}");
// Gom nhóm theo assignee
var statisticsKH = filteredIssuesKH
    .GroupBy(i => i.Assignee?.Name ?? "Unassigned")
    .Select(g => new
    {
        Assignee = g.Key,
        Total = g.Count(),
        DoiSpec = g.Count(i => i.Labels?.Contains("_Đợi Spec") == true),
        ChoLam = g.Count(i => i.Labels?.Contains("_Todo: Chờ làm") == true),
        DangLam = g.Count(i => i.Labels?.Contains("_In-Progress: Đang làm") == true),
        TamNgung = g.Count(i => i.Labels?.Contains("_Pending:  Tạm ngưng (lý do)") == true),
        Testing = g.Count(i => i.Labels?.Contains("Testing : đã xong chức năng chờ test") == true),
        Plan10 = g.Count(i => i.Labels?.Contains("Plan 10-12") == true),
        Plan1 = g.Count(i => i.Labels?.Contains("Plan 1-3 2026") == true),
        CanNotPlan = g.Count(i => i.Labels?.Contains("Can not plan (not enough information)") == true),
        NotSeenYet = g.Count() - g.Count(i => i.Labels?.Contains("Plan 10-12") == true) - g.Count(i => i.Labels?.Contains("Plan 1-3 2026") == true) - g.Count(i => i.Labels?.Contains("Can not plan (not enough information)") == true),
        ids = string.Join("", g.Select(i => i.iid.ToString() + ";"))
    })
    .OrderByDescending(g => g.ChoLam)
    .ToList();


var filteredIssuesKHBug = allIssues
    .Where(issue => issue.Labels != null && !issue.Labels.Any(label => excludedLabels.Contains(label)) && issue.Labels.Any(x => x.ToUpper().Contains("CLIENTFEEDBACK BUG")))
    .ToList();

Console.WriteLine($"✅ Tổng số issue: {filteredIssuesKHBug.Count}");
// Gom nhóm theo assignee
var statisticsKHBug = filteredIssuesKHBug
    .GroupBy(i => i.Assignee?.Name ?? "Unassigned")
    .Select(g => new
    {
        Assignee = g.Key,
        Total = g.Count(),
        DoiSpec = g.Count(i => i.Labels?.Contains("_Đợi Spec") == true),
        ChoLam = g.Count(i => i.Labels?.Contains("_Todo: Chờ làm") == true),
        DangLam = g.Count(i => i.Labels?.Contains("_In-Progress: Đang làm") == true),
        TamNgung = g.Count(i => i.Labels?.Contains("_Pending:  Tạm ngưng (lý do)") == true),
        Testing = g.Count(i => i.Labels?.Contains("Testing : đã xong chức năng chờ test") == true),
        Plan10 = g.Count(i => i.Labels?.Contains("Plan 10-12") == true),
        Plan1 = g.Count(i => i.Labels?.Contains("Plan 1-3 2026") == true),
        CanNotPlan = g.Count(i => i.Labels?.Contains("Can not plan (not enough information)") == true),
        NotSeenYet = g.Count() - g.Count(i => i.Labels?.Contains("Plan 10-12") == true)- g.Count(i => i.Labels?.Contains("Plan 1-3 2026") == true)- g.Count(i => i.Labels?.Contains("Can not plan (not enough information)") == true),
        ids = string.Join("", g.Select(i => i.iid.ToString() + ";"))
    })
    .OrderByDescending(g => g.ChoLam)
    .ToList();



var filteredIssuesKHBugDrawing = allIssues
    .Where(issue => issue.Labels != null && !issue.Labels.Any(label => excludedLabels.Contains(label)) && issue.Labels.Any(x => x.Contains("Fresco_Feedback Bug")))
    .ToList();

Console.WriteLine($"✅ Tổng số issue: {filteredIssuesKHBug.Count}");
// Gom nhóm theo assignee
var statisticsKHBugDrawing = filteredIssuesKHBugDrawing
    .GroupBy(i => i.Assignee?.Name ?? "Unassigned")
    .Select(g => new
    {
        Assignee = g.Key,
        Total = g.Count(),
        DoiSpec = g.Count(i => i.Labels?.Contains("_Đợi Spec") == true),
        ChoLam = g.Count(i => i.Labels?.Contains("_Todo: Chờ làm") == true),
        DangLam = g.Count(i => i.Labels?.Contains("_In-Progress: Đang làm") == true),
        TamNgung = g.Count(i => i.Labels?.Contains("_Pending:  Tạm ngưng (lý do)") == true),
        Testing = g.Count(i => i.Labels?.Contains("Testing : đã xong chức năng chờ test") == true),
        Plan10 = g.Count(i => i.Labels?.Contains("Plan 10-12") == true),
        Plan1 = g.Count(i => i.Labels?.Contains("Plan 1-3 2026") == true),
        CanNotPlan = g.Count(i => i.Labels?.Contains("Can not plan (not enough information)") == true),
        NotSeenYet = g.Count() - g.Count(i => i.Labels?.Contains("Plan 10-12") == true) - g.Count(i => i.Labels?.Contains("Plan 1-3 2026") == true) - g.Count(i => i.Labels?.Contains("Can not plan (not enough information)") == true),
        ids = string.Join("", g.Select(i => i.iid.ToString() + ";"))
    })
    .OrderByDescending(g => g.ChoLam)
    .ToList();




// Export ra CSV
string filePath = "gitlab_issue_summary.csv";
var lines = new List<string>();
lines.Add("======================All=================, Total,ĐơiSpec, Todo, In-Progress, Pending ,Testing,Plan 10-12,Plan 1-3 2026,Can not plan (not enough information),Not Seen Yet");

lines.AddRange(statistics.Select(s =>
    $"{s.Assignee},{s.Total},{s.DoiSpec},{s.ChoLam},{s.DangLam},{s.TamNgung},{s.Testing},{s.Plan10},{s.Plan1},{s.CanNotPlan},{s.NotSeenYet},{s.ids}"
));
lines.Add("======================Feedback KH=================, Total,ĐơiSpec, Todo, In-Progress, Pending ,Testing,Plan 10-12,Plan 1-3 2026,Can not plan (not enough information),Not Seen Yet");
lines.AddRange(statisticsKH.Select(s =>
    $"{s.Assignee},{s.Total},{s.DoiSpec},{s.ChoLam},{s.DangLam},{s.TamNgung},{s.Testing},{s.Plan10},{s.Plan1},{s.CanNotPlan},{s.NotSeenYet},{s.ids}"
));
lines.Add("====================== Feedback KH BUG =================, Total,ĐơiSpec, Todo, In-Progress, Pending ,Testing,Plan 10-12,Plan 1-3 2026,Can not plan (not enough information),Not Seen Yet");
lines.AddRange(statisticsKHBug.Select(s =>
   $"{s.Assignee},{s.Total},{s.DoiSpec},{s.ChoLam},{s.DangLam},{s.TamNgung},{s.Testing},{s.Plan10},{s.Plan1},{s.CanNotPlan},{s.NotSeenYet},{s.ids}"
));
lines.Add("====================== Feedback KH BUG Drawing=================, Total,ĐơiSpec, Todo, In-Progress, Pending ,Testing,Plan 10-12,Plan 1-3 2026,Can not plan (not enough information),Not Seen Yet");
lines.AddRange(statisticsKHBugDrawing.Select(s =>
   $"{s.Assignee},{s.Total},{s.DoiSpec},{s.ChoLam},{s.DangLam},{s.TamNgung},{s.Testing},{s.Plan10},{s.Plan1},{s.CanNotPlan},{s.NotSeenYet},{s.ids}"
));

await File.WriteAllLinesAsync(filePath, lines, new UTF8Encoding(true));
Process.Start(new ProcessStartInfo
{
    FileName = "gitlab_issue_summary.csv",
    UseShellExecute = true // Quan trọng để Windows tự mở bằng app mặc định
});

public class GitLabIssue
{
    public int iid { get; set; }
    public string Title { get; set; }
    public GitLabUser Assignee { get; set; }
    public string State { get; set; }
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("labels")]
    public List<string> Labels { get; set; }
}

public class GitLabUser
{
    public string Name { get; set; }
    public string Username { get; set; }
}