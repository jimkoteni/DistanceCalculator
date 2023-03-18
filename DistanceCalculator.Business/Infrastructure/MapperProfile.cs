using AutoMapper;
using DistanceCalculator.Business.DistanceCalculator.Models;
using DistanceCalculator.Business.Integrations.AirportService.Models;

namespace DistanceCalculator.Business.Infrastructure
{
	/// <summary>
	/// Configuration of mapper.
	/// </summary>
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<Coordinate, Point>()
				.ForMember(dest => dest.X, opts => opts.MapFrom(src => src.Latitude))
				.ForMember(dest => dest.Y, opts => opts.MapFrom(src => src.Longitude));
		}
	}
}