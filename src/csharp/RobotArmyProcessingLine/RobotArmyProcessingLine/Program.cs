using Ninject;
using Ninject.Modules;
using RobotArmyProcessingLine.DI;
using RobotArmyProcessingLine.Interface;
using System;
using System.Linq;

namespace RobotArmyProcessingLine
{
    /// <summary>
    /// Core RobotArmyProcessingLine Application.
    /// </summary>
    class Program
    {
        #region Private Static IKernel Ninject Container

        /// <summary>
        /// Represents the IKernel Ninject Container
        /// for this RobotArmyProcessingLine Application.
        /// </summary>
        private static IKernel container;

        #endregion Private Static IKernel Ninject Container

        #region Main Application Entry Point

        /// <summary>
        /// RobotArmyProcessingLine Main entry point.
        /// </summary>
        /// <param name="args">Optional input arguments for this application.</param>
        static void Main(string[] args)
        {
            // Configure the Ninject DI Container ready for use (before the application kicks in
            ConfigureNinjectForApplication();

            // Run the application 'proper'
            RunApplication();
        }

        #endregion Main Application Entry Point

        #region Ninject Static Configuration Method

        /// <summary>
        /// Private static helper method that configures the Ninject
        /// Container for use in this test application.
        /// </summary>
        private static void ConfigureNinjectForApplication()
        {
            // Create a StandardKernel object, adding in INinjectModule supporting 
            // objects (all with a load method binding interfaces to concrete types)
            container = new StandardKernel(new INinjectModule[]
            {
                new FactoryModule(),
                new FactoryUtilityModule(),
                new RobotModule()
            });
        }

        #endregion Ninject Static Configuration Methods

        #region Private Static Application Methods

        /// <summary>
        /// Private static helper method that creates a 
        /// 'Factory' and kicks off the process of deploying robots.
        /// </summary>
        private static void RunApplication()
        {
            IRobotFactory robotFactory;

            try
            {
                // Attempt to retrieve a Robot Factory (based in a mapping to the IRobotFactory interface)
                robotFactory = container.TryGet<IRobotFactory>();

                if (robotFactory != null)
                {
                    // We retrieved a valid 'Factory'. Add Robots to the Factory and 'Process' 
                    // and 'Deploy' them if the Factory is not 'oversubscribed' and is therefore functional
                    robotFactory.AddRobots(GetTestRobots());

                    if (robotFactory.FactoryOperational)
                    {
                        PerformFactoryProcessing(robotFactory);
                        DeployRobots(robotFactory);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured whilst processing/deploying Robots: Exception Message: " + ex.Message);
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Private static helper test method that generates
        /// a basic list of test Robots.
        /// </summary>
        /// <returns>A List of IRobot supporting objects.</returns>
        private static IRobot[] GetTestRobots()
        {
            return new IRobot[]
            {
                (IRobot)container.TryGet<ISmallRobot>(),
                (IRobot)container.TryGet<ISmallRobot>(),
                (IRobot)container.TryGet<ISmallRobot>(),
                (IRobot)container.TryGet<ISmallRobot>(),
                (IRobot)container.TryGet<ILargeRobot>(),
                (IRobot)container.TryGet<ILargeRobot>(),
                (IRobot)container.TryGet<ILargeRobot>(),
                (IRobot)container.TryGet<ILargeRobot>()
            };
        }

        /// <summary>
        /// Private static helper that triggers a Robot Factories
        /// Paint/AttachWeapons methods (affecting Robots in the Factory).
        /// </summary>
        /// <param name="robotFactory">The Robot Factory to trigger actions on.</param>
        private static void PerformFactoryProcessing(IRobotFactory robotFactory)
        {
            if (robotFactory != null)
            {
                robotFactory.PaintRobots();
                robotFactory.AttachWeapons();
            }
        }

        /// <summary>
        /// Private static helper that triggers a Robot Factories
        /// DeployRobots method (affecting Robots in the Factory).
        /// </summary>
        /// <param name="robotFactory">The Robot Factory to trigger the action on.</param>
        private static void DeployRobots(IRobotFactory robotFactory)
        {
            // If Factory is not null then deploy the Robots
            robotFactory?.DeployRobots().ToList().ForEach(robot =>
            {
                // If each robot is not null then trigger various robot behaviours (move, speak, attack, etc) and 'output', depending on implementation
                if (robot != null)
                {
                    robot.Move();
                    robot.Speak();
                    robot.Attack();

                    if (robot is ISmallRobot)
                    {
                        ((ISmallRobot)robot).Sneak();
                    }
                    else if (robot is ILargeRobot)
                    {
                        ((ILargeRobot)robot).Crush();
                    }
                }

                Console.WriteLine();
            });
        }

        #endregion Private Static Application Methods
    }
}