using Ninject.Modules;
using RobotArmyProcessingLine.Interface;
using RobotArmyProcessingLine.Model;

namespace RobotArmyProcessingLine.DI
{
    /// <summary>
    /// Denotes a Ninject Factory Utility Module 
    /// determining the mappings between Robot 
    /// Factory Utility Interfaces and associated Concrete Types. 
    /// </summary>
    public class FactoryUtilityModule : NinjectModule
    {
        #region Overriden Load Method

        /// <summary>
        /// Overriden Load method to bind services
        /// to concrete types.
        /// </summary>
        public override void Load()
        {
            // A 'Factory' is able to paint robots and attach weapons. Map services (interfaces) to concrete
            // types for when a 'get' on the Ninject IoC container is used
            Bind<IPainter>().To<BlueWaterPaintMachine>().InSingletonScope();
            Bind<IWeaponAttacher>().To<RocketLauncherAttacher>().InSingletonScope();
        }

        #endregion Overriden Load Method
    }
}
