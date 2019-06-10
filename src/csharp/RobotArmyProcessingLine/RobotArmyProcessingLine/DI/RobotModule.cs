using Ninject.Modules;
using RobotArmyProcessingLine.Interface;
using RobotArmyProcessingLine.Model;

namespace RobotArmyProcessingLine.DI
{
    /// <summary>
    /// Denotes a Ninject Robot Module 
    /// determining the mappings between Robot 
    /// Interfaces and associated Concrete Types. 
    /// </summary>
    public class RobotModule : NinjectModule
    {
        #region Overriden Load Method

        /// <summary>
        /// Overriden Load method to bind services
        /// to concrete types.
        /// </summary>
        public override void Load()
        {
            // Determine the concrete types associated with ISmallRobot and ILargeRobot
            Bind<ISmallRobot>().To<SmallRobotV1>();
            Bind<ILargeRobot>().To<LargeRobotV1>();

            // Something a bit different here. We bind slightly different concrete types to IRobotOutputter based on
            // the type it is being 'injected' into (determines how robots output information about commands they undertake)
            Bind<IRobotOutputter>().To<RobotOutput>().WhenInjectedInto<ISmallRobot>().InSingletonScope();
            Bind<IRobotOutputter>().To<RobotFileOutput>().WhenInjectedInto<ILargeRobot>().InSingletonScope();
        }

        #endregion Overriden Load Method
    }
}