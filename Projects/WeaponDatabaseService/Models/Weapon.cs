namespace WeaponDatabaseService
{
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slot { get; set; }
        public string Type { get; set; }
        public int? EffectId { get; set; }
        public string Effect { get; set; }
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }
    }
};