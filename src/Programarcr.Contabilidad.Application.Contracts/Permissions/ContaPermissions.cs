namespace Programarcr.Contabilidad.Permissions
{
    public static class ContaPermissions
    {
        public const string AccountPermissionGroup = "Contabilidad";

        public static class Account
        {
            public const string Default = AccountPermissionGroup + ".Account";
            public const string CreateEdit = Default + ".CreateEdit";
            public const string Plan = Default + ".Plan";
        }

        public const string ConfigPermissionGroup = "Config";

        public static class Monedas
        {
            public const string Default = ConfigPermissionGroup + ".Monedas";
            public const string CreateEdit = Default + ".CreateEdit";
        }

        public static class CentroDeCostos
        {
            public const string Default = ConfigPermissionGroup + ".CentroDeCostos";
            public const string CreateEdit = Default + ".CreateEdit";
        }

        
        public static class Cotizacion
        {
            public const string Default = ConfigPermissionGroup + ".Cotizacion";
            public const string CreateEdit = Default + ".CreateEdit";
            public const string Delete = Default + ".Delete";
        }
        public static class PeriodoContable
        {
            public const string Default = ConfigPermissionGroup + ".PeriodoContable";
        }
    }
}