﻿using AutoMapper;
using Clinica.Application.Interface.Interfaces;
using Clinica.Application.Interface.Services;
using Clinica.Application.UseCase.Commons.Bases;
using Clinica.Domain.Entities;
using Clinica.Utilities.Constants;
using MediatR;
using Entity = Clinica.Domain.Entities;

namespace Clinica.Application.UseCase.UseCases.Result.Commands.CreateCommand
{
    public class CreateResultHandler : IRequestHandler<CreateResultCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileStorage _fileStorage;

        public CreateResultHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileStorage fileStorage)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileStorage = fileStorage;
        }

        public async Task<BaseResponse<bool>> Handle(CreateResultCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            using var transaction = _unitOfWork.BeginTransaction();

            try
            {
                var result = _mapper.Map<Entity.Result>(request);
                var resultReg = await _unitOfWork.Result.RegisterResult(result);

                foreach (var resultFile in request.ResultDetails)
                {
                    var newResultDetail = new ResultDetail
                    {
                        ResultId = resultReg.ResultId,
                        ResultFile = await _fileStorage.SaveFile(FileServerContainers.RESULT_FILES, resultFile.ResultFile!),
                        TakeExamDetailId = resultFile.TakeExamDetailId
                    };
                    await _unitOfWork.Result.RegisterResultDetail(newResultDetail);
                }

                transaction.Complete();
                response.IsSuccess = true;
                response.Message = GlobalMessage.MESSAGE_SAVE;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
