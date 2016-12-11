using Kenrapid.CRM.Web.Domain;
using Kenrapid.CRM.Web.Infrastructure.Mapping;
using System.Linq;
namespace Kenrapid.CRM.Web.Models.Issue
{
	public class AssignmentStatsViewModel:IHaveCustomMappings
	{
		public string UserName { get; set; }
		public int Enhancements { get; set; }
		public int Bugs { get; set; }
		public int Support { get; set; }
		public int Other { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<ApplicationUser, AssignmentStatsViewModel>()
                .ForMember(m=>m.Enhancements,
                    opt=>opt.MapFrom(u=>u.Assignments.Count(i=>i.IssueType==IssueType.Enhancement)))
                .ForMember(m=>m.Bugs,
                    opt=>opt.MapFrom(u=>u.Assignments.Count(i=>i.IssueType==IssueType.Bug)))
                .ForMember(m=>m.Support,
                    opt=>opt.MapFrom(u=>u.Assignments.Count(i=>i.IssueType==IssueType.Support)))
                .ForMember(m=>m.Other,
                    opt=>opt.MapFrom(u=>u.Assignments.Count(i=>i.IssueType==IssueType.Other)));
        }
    }
}