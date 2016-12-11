using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Kenrapid.CRM.Web.Infrastructure.Mapping;
using Kenrapid.CRM.Web.Domain;

namespace Kenrapid.CRM.Web.Models
{
	public class ProfileForm : IMapFrom<ApplicationUser>, IHaveCustomMappings
	{
		public string FullName { get; set; }

		public string EmailAddress { get; set; }

		public void CreateMappings(IConfiguration configuration)
		{
			configuration.CreateMap<ApplicationUser, ProfileForm>()
				.ForMember(d => d.FullName, opt => opt.MapFrom(s => s.UserName))
				.ForMember(d => d.EmailAddress, opt => opt.MapFrom(s => s.Email));
		}
	}
}