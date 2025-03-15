using System.Collections.Generic;

namespace WinterUniverse
{
    public class StatHolder
    {
        private List<Stat> _stats;
        private Stat _healthMax;
        private Stat _healthRegeneration;
        private Stat _staminaMax;
        private Stat _staminaRegeneration;
        private Stat _slicingResistance;
        private Stat _piercingResistance;
        private Stat _bluntResistance;
        private Stat _thermalResistance;
        private Stat _electricalResistance;
        private Stat _chemicalResistance;

        public List<Stat> Stats => _stats;
        public Stat HealthMax => _healthMax;
        public Stat HealthRegeneration => _healthRegeneration;
        public Stat StaminaMax => _staminaMax;
        public Stat StaminaRegeneration => _staminaRegeneration;
        public Stat SlicingResistance => _slicingResistance;
        public Stat PiercingResistance => _piercingResistance;
        public Stat BluntResistance => _bluntResistance;
        public Stat ThermalResistance => _thermalResistance;
        public Stat ElectricalResistance => _electricalResistance;
        public Stat ChemicalResistance => _chemicalResistance;

        public StatHolder(List<StatCreator> stats)
        {
            _stats = new();
            foreach (StatCreator stat in stats)
            {
                _stats.Add(new(stat.Config, stat.BaseValue));
            }
            AssignStats();
            RecalculateStats();
        }

        private void AssignStats()
        {
            foreach (Stat s in _stats)
            {
                switch (s.Config.ID)
                {
                    case "HPMAX":
                        _healthMax = s;
                        break;
                    case "HPREGEN":
                        _healthRegeneration = s;
                        break;
                    case "SPMAX":
                        _staminaMax = s;
                        break;
                    case "SPREGEN":
                        _staminaRegeneration = s;
                        break;
                    case "SLICINGRES":
                        _slicingResistance = s;
                        break;
                    case "PIERCINGRES":
                        _piercingResistance = s;
                        break;
                    case "BLUNTRES":
                        _bluntResistance = s;
                        break;
                    case "THERMALRES":
                        _thermalResistance = s;
                        break;
                    case "ELECTRICALRES":
                        _electricalResistance = s;
                        break;
                    case "CHEMICALRES":
                        _chemicalResistance = s;
                        break;

                }
            }
        }

        public void RecalculateStats()
        {
            foreach (Stat s in _stats)
            {
                s.CalculateCurrentValue();
            }
        }

        public Stat GetStat(string name)
        {
            foreach (Stat s in _stats)
            {
                if (s.Config.ID == name)
                {
                    return s;
                }
            }
            return null;
        }

        public void AddStatModifiers(List<StatModifierCreator> modifiers)
        {
            foreach (StatModifierCreator smc in modifiers)
            {
                AddStatModifier(smc);
            }
            RecalculateStats();
        }

        public void AddStatModifier(StatModifierCreator smc)
        {
            GetStat(smc.Config.ID).AddModifier(smc.Modifier);
        }

        public void RemoveStatModifiers(List<StatModifierCreator> modifiers)
        {
            foreach (StatModifierCreator smc in modifiers)
            {
                RemoveStatModifier(smc);
            }
            RecalculateStats();
        }

        public void RemoveStatModifier(StatModifierCreator smc)
        {
            GetStat(smc.Config.ID).RemoveModifier(smc.Modifier);
        }
    }
}