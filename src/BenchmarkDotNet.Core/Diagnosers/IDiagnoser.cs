﻿using System.Collections.Generic;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Validators;

namespace BenchmarkDotNet.Diagnosers
{
    public interface IDiagnoser
    {
        bool IsExtraRunRequired { get; }

        IEnumerable<string> Ids { get; } 

        IEnumerable<IExporter> Exporters { get; }
            
        IColumnProvider GetColumnProvider();

        /// <summary>
        /// before jitting, warmup
        /// </summary>
        void BeforeAnythingElse(DiagnoserActionParameters parameters);

        /// <summary>
        /// after globalSetup, before run
        /// </summary>
        void AfterGlobalSetup(DiagnoserActionParameters parameters);

        /// <summary>
        /// after globalSetup, warmup and pilot but before the main run
        /// </summary>
        void BeforeMainRun(DiagnoserActionParameters parameters);

        /// <summary>
        /// after run, before globalSleanup
        /// </summary>
        void BeforeGlobalCleanup(DiagnoserActionParameters parameters);

        void ProcessResults(Benchmark benchmark, BenchmarkReport report);

        void DisplayResults(ILogger logger);

        IEnumerable<ValidationError> Validate(ValidationParameters validationParameters);
    }

    public interface IConfigurableDiagnoser<TConfig> : IDiagnoser
    {
        IConfigurableDiagnoser<TConfig> Configure(TConfig config);
    }
}
