﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartIdeia.Src.Modules.Accounts.UseCases
{
    public class CreateRandomTokenUseCase
    {
        private static Random random;

        public CreateRandomTokenUseCase()
        {
            random = new Random();
        }

        public string Execute(int length = 30)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
