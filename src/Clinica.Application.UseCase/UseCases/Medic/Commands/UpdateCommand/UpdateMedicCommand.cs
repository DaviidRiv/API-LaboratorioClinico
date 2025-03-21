﻿using Clinica.Application.UseCase.Commons.Bases;
using MediatR;

namespace Clinica.Application.UseCase.UseCases.Medic.Commands.UpdateCommand
{
    public class UpdateMedicCommand : IRequest<BaseResponse<bool>>
    {
        public int MedicId { get; set; }
        public string? Names { get; set; }
        public string? LastName { get; set; }
        public string? MotherMaidenName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? BirthDate { get; set; }
        public int? DocumentTypeId { get; set; }
        public string? DocumentNumber { get; set; }
        public int? SpecialtyId { get; set; }
    }
}
