using System;

namespace AttacksDatabaseService
{
    public class Attack
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public string WeaponRequirement { get; set; }
    }
}
