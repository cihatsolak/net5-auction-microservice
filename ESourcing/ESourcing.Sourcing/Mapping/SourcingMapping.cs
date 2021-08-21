using AutoMapper;
using ESourcing.Sourcing.Entities;
using EventBusRabbitMQ.Events;

namespace ESourcing.Sourcing.Mapping
{
    public class SourcingMapping : Profile
    {
        public SourcingMapping()
        {
            AuctionMapping();
        }

        private void AuctionMapping()
        {
            CreateMap<OrderCreateEvent, Bid>().ReverseMap();
        }
    }
}
