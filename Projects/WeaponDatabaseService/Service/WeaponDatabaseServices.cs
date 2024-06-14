using System;
using Sentry;
using WeaponModel;

namespace WeaponDatabaseService
{
    public class WeaponDatabaseServices
    {
        private readonly IHub _sentryHub;
        private PreparedStatements ps = PreparedStatements.CreateInstance();

        public WeaponDatabaseServices()
        {

        }

        public bool AddWeapon(Weapon weapon)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("AddWeapon");
            try
            {
                return ps.InsertWeapon(weapon);
            }
            catch (Exception e)
            {
                _sentryHub.CaptureException(e);
                childSpan?.Finish(e);
                Console.WriteLine($"Unexpected error: {e.Message}");
                return false;
            }
        }

        public bool UpdateWeapon(Weapon weapon)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("UpdateWeapon");
            try
            {
                return ps.UpdateWeapon(weapon);
            }
            catch (Exception e)
            {
                _sentryHub.CaptureException(e);
                childSpan?.Finish(e);
                Console.WriteLine($"Unexpected error: {e.Message}");
                return false;
            }
        }

        public bool DeleteWeapon(int id)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("DeleteWeapon");
            try
            {
                return ps.DeleteWeapon(id);
            }
            catch (Exception e)
            {
                _sentryHub.CaptureException(e);
                childSpan?.Finish(e);
                Console.WriteLine($"Unexpected error: {e.Message}");
                return false;
            }
        }

        public Weapon GetWeaponById(int id)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("GetWeaponById");
            try
            {
                return ps.GetWeaponById(id);
            }
            catch (Exception e)
            {
                _sentryHub.CaptureException(e);
                childSpan?.Finish(e);
                Console.WriteLine($"Unexpected error: {e.Message}");
                return null;
            }
        }

        public List<Weapon> GetAllWeapons()
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("GetAllWeapons");
            try
            {
                return ps.GetAllWeapons();
            }
            catch (Exception e)
            {
                _sentryHub.CaptureException(e);
                childSpan?.Finish(e);
                Console.WriteLine($"Unexpected error: {e.Message}");
                return null;
            }
        }

        public List<Weapon> GetWeaponsByType(string type)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("GetWeaponsByType");
            try
            {
                return ps.GetWeaponsByType(type);
            }
            catch (Exception e)
            {
                _sentryHub.CaptureException(e);
                childSpan?.Finish(e);
                Console.WriteLine($"Unexpected error: {e.Message}");
                return null;
            }
        }
    }
}
