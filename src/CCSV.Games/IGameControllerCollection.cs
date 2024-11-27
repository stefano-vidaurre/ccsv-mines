﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCSV.Games;
public interface IGameControllerCollection : IEnumerable<Type>
{
    IGameControllerCollection SetMain<TController>() where TController : class, IGameController;
    public IGameControllerCollection AddGameController<TController, TImplementation>() where TController : class, IGameController where TImplementation : class, TController;
}