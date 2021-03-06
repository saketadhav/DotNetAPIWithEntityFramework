﻿using Application.Common.Mappings;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Clients.Queries.GetClients
{
    public class ClientDto : IMapFrom<Client>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Fund> Funds { get; set; }
    }
}
