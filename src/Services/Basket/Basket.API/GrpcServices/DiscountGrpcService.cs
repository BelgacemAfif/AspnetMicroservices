﻿using Discount.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        {
            _discountProtoService = discountProtoService;
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            var getDiscountRequest = new GetDiscountRequest() { ProductName = productName };
            //getDiscountRequest.ProductName = productName;
            var couponModel = await _discountProtoService.GetDiscountAsync(getDiscountRequest);
            return couponModel;
        }
        
    }
}
