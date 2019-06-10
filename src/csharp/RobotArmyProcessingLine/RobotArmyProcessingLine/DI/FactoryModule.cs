using Ninject.Modules;
using RobotArmyProcessingLine.Interface;
using RobotArmyProcessingLine.Production;

namespace RobotArmyProcessingLine.DI
{
    /// <summary>
    /// Denotes a Ninject Factory Module 
    /// determining the mappings between Robot 
    /// Factory Interfaces and associated Concrete Types.
    /// </summary>
    public class FactoryModule : NinjectModule
    {
        #region Overriden Load Method

        /// <summary>
        /// Overriden Load method to bind services
        /// to concrete types.
        /// </summary>
        public override void Load()
        {
            // Bind IRobotFactory to a concrete type (in this case, as a singleton - i.e. same instance return when the container
            // is asked to 'get' an instance based on the interface)
            Bind<IRobotFactory>().To<LargeRobotFactory>().InSingletonScope();
        }

        #endregion Overriden Load Method
    }
}