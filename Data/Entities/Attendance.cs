namespace xilopro2.Data.Entities
{
    public class Attendance
    {
        public int Id { get; set; }
        public int PlayerId { get; set; } // ID del jugador
        public string PlayerName { get; set; } // Nombre del jugador
        public string Nota { get; set; }
        public DateTime AttendanceDate { get; set; } // Fecha de asistencia u otro campo relevante
        public AttendanceStatus Status { get; set; }

        public Player? Player { get; set; }

    }
    public enum AttendanceStatus
    {
        Present,
        Absent,
        Justified
    }
}
