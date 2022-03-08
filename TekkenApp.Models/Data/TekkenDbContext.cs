using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Models;

#nullable disable

namespace TekkenApp.Data
{
    public partial class TekkenDbContext : DbContext
    {
        public TekkenDbContext() { }
        public TekkenDbContext(DbContextOptions<TekkenDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<BaseUtil> BaseUtil { get; set; }
        public virtual DbSet<BaseDataEntity> BaseEntities { get; set; }
        public virtual DbSet<Move> Move { get; set; }
        public virtual DbSet<Move_name> Move_name { get; set; }
        public virtual DbSet<MoveData> MoveData { get; set; }
        public virtual DbSet<MoveData_name> MoveData_Name { get; set; }
        public virtual DbSet<MoveCommand> MoveCommand { get; set; }
        public virtual DbSet<MoveCommand_name> MoveCommand_name { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<State_name> State_name { get; set; }
        public virtual DbSet<StateGroup> StateGroup { get; set; }
        public virtual DbSet<StateGroup_name> StateGroup_name { get; set; }
        public virtual DbSet<Character> character { get; set; }
        public virtual DbSet<Character_name> character_name { get; set; }
        public virtual DbSet<Command> Command { get; set; }
        public virtual DbSet<Command_name> Command_name { get; set; }
        public virtual DbSet<HitType> HitType { get; set; }
        public virtual DbSet<HitType_name> HitType_name { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<MoveSubType> moveSubType { get; set; }
        public virtual DbSet<MoveSubType_name> moveSubType_name { get; set; }
        public virtual DbSet<MoveText> moveText { get; set; }
        public virtual DbSet<MoveText_name> moveText_name { get; set; }
        public virtual DbSet<MoveType> moveType { get; set; }
        public virtual DbSet<MoveType_name> moveType_name { get; set; }

        public virtual DbSet<TableCode> tableCode { get; set; }
        //public virtual DbSet<TekkenVersion> tekkenVersion { get; set; }
        public DbSet<Dictionary<string, object>> Products => Set<Dictionary<string, object>>("Product");



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = Tekken; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False", b => b.MigrationsAssembly("TekkenApp")).EnableSensitiveDataLogging(true);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.SharedTypeEntity<Dictionary<string, object>>("moveType_name", b =>
            //{
            //    //b.IndexerProperty<int>("Id");
            //    b.IndexerProperty<string>("Name").IsRequired();
            //    b.IndexerProperty<decimal>("Price");
            //});

            modelBuilder.HasAnnotation("Relational:Collation", "Korean_Wansung_BIN2");

            modelBuilder.Entity<BaseUtil>(entity =>
            {

            });
            modelBuilder.Entity<Language>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Description).IsUnicode(false);
            });

            #region Move
            modelBuilder.Entity<Move>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.HasMany(d => d.NameSet as HashSet<Move_name>)
                    .WithOne()
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.Base_code)
                    .HasConstraintName("FK_move_name_Move")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.MoveCommand)
                    .WithOne(p => p.Move)
                    .HasPrincipalKey<Move>(p => p.Code)
                    .HasForeignKey<MoveCommand>(d => d.Base_code)
                    .HasConstraintName("FK_MoveCommand_Move");
                //.OnDelete(DeleteBehavior.Cascade);

                //entity.HasOne(d => d.character_codeNavigation)
                //    .WithMany(p => p.Move)
                //    .HasPrincipalKey(p => p.code)
                //    .HasForeignKey(d => d.character_code)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Move_character");

                //entity.HasOne(d => d.versionNavigation)
                //    .WithMany(p => p.Move)
                //    .HasForeignKey(d => d.version)
                //    .HasConstraintName("FK_Move_version");
            });


            #region MoveCommand
            modelBuilder.Entity<MoveCommand>(entity =>
            {
                entity.Property(e => e.Command).IsUnicode(false);

                // entity.HasMany(d => d.NameSet)
                //.WithOne()
                //.HasPrincipalKey(p => p.Code)
                //.HasForeignKey(d => d.Base_code)
                //.HasConstraintName("FK_moveCommand_name_moveCommand")
                //.OnDelete(DeleteBehavior.Cascade);



                entity.HasOne(d => d.Move)
               .WithOne(p => p.MoveCommand)
               .HasPrincipalKey<MoveCommand>(c => c.Base_code)
               .HasForeignKey<Move>(m => m.Code)
               .HasConstraintName("fk_movecommand_move");
                //.ondelete(deletebehavior.cascade);


                entity.HasMany(d => d.NameSet as HashSet<MoveCommand_name>)
               .WithOne()
               .HasPrincipalKey(p => p.Code)
               .HasForeignKey(d => d.Base_code)
               .HasConstraintName("FK_moveCommand_Move")
               .OnDelete(DeleteBehavior.Cascade);




                //entity.HasOne(d => d.move_codeNavigation)
                //    .WithOne(p => p.Move_command)
                //    .HasPrincipalKey<Move>(p => p.code)
                //    .HasForeignKey<Move_command>(d => d.move_code)
                //    .HasConstraintName("FK_Move_command_Move1");
            });
            modelBuilder.Entity<MoveCommand_name>(entity =>
            {
                entity.Property(e => e.Language_code)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Name).IsUnicode(false);

                //entity.HasOne(d => d.Move_Command_codeNavigation)
                //    .WithMany(p => p.move_command_name)
                //    .HasPrincipalKey(p => p.code)
                //    .HasForeignKey(d => d.Move_Command_code)
                //    .HasConstraintName("FK_move_command_Move");

                //entity.HasOne(d => d.language_codeNavigation)
                //    .WithMany(p => p.move_command_name)
                //    .HasPrincipalKey(p => p.code)
                //    .HasForeignKey(d => d.language_code)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_move_command_language");
            });
            #endregion MoveCommand

            #region MoveData
            modelBuilder.Entity<MoveData>(entity =>
            {

                entity.HasMany(d => d.NameSet as HashSet<MoveData_name>)
                    .WithOne()
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.Base_code)
                    // .HasConstraintName("FK_moveData_Move")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(m => m.Move)
                .WithOne(p => p.MoveData)
                .HasPrincipalKey<MoveData>(m => m.Base_code)
                .HasForeignKey<Move>(p => p.Code);
                //.HasConstraintName("fk_movecommand_move");

                //.ondelete(deletebehavior.cascade);


                //entity.HasOne(d => d.counterType_codeNavigation)
                //    .WithMany(p => p.move_datacounterType_codeNavigation)
                //    .HasPrincipalKey(p => p.Code)
                //    .HasForeignKey(d => d.counterType_code)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_move_data_hitType3");

                //entity.HasOne(d => d.guardType_codeNavigation)
                //    .WithMany(p => p.move_dataguardType_codeNavigation)
                //    .HasPrincipalKey(p => p.Code)
                //    .HasForeignKey(d => d.guardType_code)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_move_data_hitType");

                //entity.HasOne(d => d.hitType_codeNavigation)
                //    .WithMany(p => p.move_datahitType_codeNavigation)
                //    .HasPrincipalKey(p => p.Code)
                //    .HasForeignKey(d => d.hitType_code)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_move_data_hitType2");
                /*
                entity.HasOne(d => d.moveSubType_codeNavigation)
                    .WithMany(p => p.move_data)
                    .HasPrincipalKey(p => p.code)
                    .HasForeignKey(d => d.moveSubType_code)
                    .HasConstraintName("FK_move_data_moveSubType");*/

                //entity.HasOne(d => d.moveType_codeNavigation)
                //    .WithMany(p => p.move_data)
                //    .HasPrincipalKey(p => p.code)
                //    .HasForeignKey(d => d.moveType_code)
                //    .HasConstraintName("FK_move_data_moveType");

                //entity.HasOne(d => d.startType_codeNavigation)
                //    .WithMany(p => p.move_datastartType_codeNavigation)
                //    .HasPrincipalKey(p => p.Code)
                //    .HasForeignKey(d => d.startType_code)
                //    .HasConstraintName("FK_move_data_hitType1");
            });
            modelBuilder.Entity<MoveData_name>(entity =>
            {
                entity.Property(e => e.Language_code)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                //entity.HasOne(d => d.Move_Data_CodeNavigation)
                //    .WithMany(p => p.NameSet)
                //    .HasPrincipalKey(p => p.Move_Code)
                //    .HasForeignKey(d => d.MoveData_Code)
                //    .HasConstraintName("FK_Move_Data_Name_move_data");
            });

            #endregion Move_data


            modelBuilder.Entity<Move_name>(entity =>
            {
                entity.Property(e => e.Language_code)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Name).IsUnicode(false);


                entity.HasOne(d => d.BaseData as Move)
              .WithMany(p => p.NameSet as IEnumerable<Move_name>)
              .HasPrincipalKey(n => n.Code)
              .HasForeignKey(m => m.Base_code);
                //.HasConstraintName("fk_movecommand_move");

                /* entity.HasOne(d => d.Move)
                 .WithOne(p => p.MoveCommand)
                 .HasPrincipalKey<MoveCommand>(c => c.Base_code)
                 .HasForeignKey<Move>(m => m.Code)
                 .HasConstraintName("fk_movecommand_move");*/

                //entity.HasOne(d => d.language_codeNavigation)
                //    .WithMany(p => p.move_name)
                //    .HasPrincipalKey(p => p.code)
                //    .HasForeignKey(d => d.language_code)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_move_name_language");

                //entity.HasOne(d => d.move_codeNavigation)
                //    .WithMany(p => p.move_name)
                //    .HasPrincipalKey(p => p.code)
                //    .HasForeignKey(d => d.move_code)
                //    .HasConstraintName("FK_move_name_Move");
            });
            #endregion Move

            #region State
            modelBuilder.Entity<State>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);
                entity.HasMany(d => d.NameSet as HashSet<State_name>)
                    .WithOne()
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.Base_code)
                    .HasConstraintName("FK_state_name_State")
                    .OnDelete(DeleteBehavior.Cascade);


                //entity.HasOne(d => d.StateGroup_codeNavigation)
                //    .WithMany(p => p.State)
                //    .HasPrincipalKey(p => p.Code)
                //    .HasForeignKey(d => d.StateGroup_code)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_State_StateGroup");
            });
            modelBuilder.Entity<State_name>(entity =>
            {
                entity.Property(e => e.Language_code)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Name).IsUnicode(false);

                //entity.HasOne(d => d.language_codeNavigation)
                //    .WithMany(p => p.State_name)
                //    .HasPrincipalKey(p => p.code)
                //    .HasForeignKey(d => d.Language_code)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_State_name_language");

                //entity.HasOne(d => d.state_codeNavigation)
                //    .WithMany(p => p.NameSet)
                //    .HasPrincipalKey(p => p.Code)
                //    .HasForeignKey(d => d.Base_code)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_State_name_State");
            });
            #endregion State

            #region StateGroup
            modelBuilder.Entity<StateGroup>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);


                entity.HasMany(d => d.NameSet as HashSet<StateGroup_name>)
                .WithOne()
                .HasPrincipalKey(p => p.Code)
                .HasForeignKey(d => d.Base_code)
                .HasConstraintName("FK_StateGroup_name_StateGroup")
                .OnDelete(DeleteBehavior.Cascade);

            });
            modelBuilder.Entity<StateGroup_name>(entity =>
            {
                entity.Property(e => e.Language_code)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Name).IsUnicode(false);




                //entity.HasOne(d => d.language_codeNavigation)
                //    .WithMany(p => p.StateGroup_name)
                //    .HasPrincipalKey(p => p.code)
                //    .HasForeignKey(d => d.Language_code)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_StateGroup_name_language");
            });
            #endregion StateGroup

            #region Character
            modelBuilder.Entity<Character>(entity =>
            {
                entity.Property(e => e.Code).IsUnicode(false).IsFixedLength(true);
                entity.Property(e => e.Description).IsUnicode(false);

                entity.HasMany(d => d.NameSet)
                    .WithOne()
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.Base_code)
                    .HasConstraintName("FK_character_name_character")
                    .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<Character_name>(entity =>
            {
                entity.Property(e => e.fullName).IsUnicode(false);

                entity.Property(e => e.Language_code)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                //entity.HasOne(d => d.language_codeNavigation)
                //    .WithMany(p => p.character_name)
                //    .HasPrincipalKey(p => p.code)
                //    .HasForeignKey(d => d.language_code)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_character_name_language");
            });
            #endregion Character

            #region Command
            modelBuilder.Entity<Command>(entity =>
            {
                entity.Property(e => e.CommandCode).IsUnicode(false).IsFixedLength(true);

                //entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.key).IsUnicode(false);

                entity.HasMany(d => d.NameSet)
                 .WithOne()
                 .HasPrincipalKey(p => p.CommandCode)
                 .HasForeignKey(d => d.CommandCode)
                 //.HasConstraintName("FK_character_name_character")
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Command_name>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Language_code)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Name).IsUnicode(false);

                //entity.HasOne(d => d.language_codeNavigation)
                //    .WithMany(p => p.command_name)
                //    .HasPrincipalKey(p => p.code)
                //    .HasForeignKey(d => d.language_code)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_command_name_language");
            });
            #endregion Command

            #region HitType
            modelBuilder.Entity<HitType>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.HasMany(d => d.NameSet as List<HitType_name>)
                    .WithOne()
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.Base_code);
                // .HasConstraintName("FK_hitType_name_hitType")
                //.OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<HitType_name>(entity =>
            {


                entity.Property(e => e.Language_code)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Name).IsUnicode(false);


                //entity.HasOne(d => d.hitType_codeNavigation)
                //    .WithMany(p => p.NameSet)
                //    .HasPrincipalKey(p => p.Code)
                //    .HasForeignKey(d => d.Base_code)
                //    .HasConstraintName("FK_hitType_name_hitType")
                //    .OnDelete(DeleteBehavior.Cascade);
            });
            #endregion HitType


            #region MoveSubType
            modelBuilder.Entity<MoveSubType>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.HasMany(d => d.NameSet as HashSet<MoveSubType_name>)
                    .WithOne()
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.Base_code)
                    //.HasConstraintName("FK_moveText_name_moveText")
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<MoveSubType_name>(entity =>
            {
                entity.Property(e => e.Language_code)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Name).IsUnicode(false);

                //entity.HasOne(d => d.language_codeNavigation)
                //    .WithMany(p => p.moveSubType_name)
                //    .HasPrincipalKey(p => p.code)
                //    .HasForeignKey(d => d.language_code)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_moveSubType_name_language");

                //entity.HasOne(d => d.moveSubType_codeNavigation)
                //.WithMany(p => p.moveSubType_name)
                //.HasPrincipalKey(p => p.code)
                //.HasForeignKey(d => d.moveSubType_code)
                //.OnDelete(DeleteBehavior.ClientSetNull)
                //.HasConstraintName("FK_moveSubType_name_moveSubType_name");
            });
            #endregion MoveSubType


            #region MoveText
            modelBuilder.Entity<MoveText>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.HasMany(d => d.NameSet as HashSet<MoveText_name>)
                    .WithOne()
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.Base_code)
                    .HasConstraintName("FK_moveText_name_moveText")
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<MoveText_name>(entity =>
            {
                entity.Property(e => e.Language_code)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Name).IsUnicode(false);

                //entity.HasOne(d => d.Base_code)
                //    .WithMany(p => p.)
                //    .HasPrincipalKey(p => p.Code)
                //    .HasForeignKey(d => d.Base_code)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_moveText_name_moveText")
                //    .OnDelete(DeleteBehavior.Cascade);

                //entity.HasOne(d => d.moveText_codeNavigation)
                //    .WithMany(p => p.moveText_name)
                //    .HasPrincipalKey(p => p.code)
                //    .HasForeignKey(d => d.moveText_code)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_moveText_name_moveText");
            });
            #endregion MoveText

            #region MoveType
            modelBuilder.Entity<MoveType>(entity =>
            {
                entity.Property(e => e.Description).IsUnicode(false);

                entity.HasMany(d => d.NameSet as HashSet<MoveType_name>)
                .WithOne()
                .HasPrincipalKey(p => p.Code)
                .HasForeignKey(d => d.Base_code)
                .HasConstraintName("FK_moveType_name_moveType")
                .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<MoveType_name>(entity =>
            {
                entity.Property(e => e.Language_code)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Name).IsUnicode(false);

                //entity.HasOne(d => d.language_codeNavigation)
                //    .WithMany(p => p.moveType_name)
                //    .HasPrincipalKey(p => p.code)
                //    .HasForeignKey(d => d.language_code)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_moveType_name_language");

                //entity.HasOne(d => d.moveType_codeNavigation)
                //    .WithMany(p => p.moveType_name)
                //    .HasPrincipalKey(p => p.code)
                //    .HasForeignKey(d => d.moveType_code)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_moveType_name_moveType");
            });
            #endregion MoveType

            modelBuilder.Entity<TableCode>(entity =>
            {
                entity.Property(e => e.tableName).IsUnicode(false);
            });

            //modelBuilder.Entity<TekkenVersion>(entity =>
            //{
            //    entity.HasKey(e => e.version)
            //        .HasName("PK_version");
            //});

            OnModelCreatingPartial(modelBuilder);


        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}