// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using warehouse.Database;

namespace warehouse.Database.Migrations
{
    [DbContext(typeof(WarehouseDbContext))]
    [Migration("20210726174415_addingOrderStatus")]
    partial class addingOrderStatus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("warehouse.Database.Entity.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("warehouse.Database.Entity.IndexItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("IndexItems");
                });

            modelBuilder.Entity("warehouse.Database.Entity.Items", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ActualLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EAN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasSerialNumber")
                        .HasColumnType("bit");

                    b.Property<int?>("IndexItemId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WhoCreatedId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IndexItemId");

                    b.HasIndex("WhoCreatedId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("warehouse.Database.Entity.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TargetLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("WhoCreatedId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("WhoCreatedId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("warehouse.Database.Entity.OrderDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ItemsId")
                        .HasColumnType("int");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemsId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("warehouse.Database.Entity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("warehouse.Database.Entity.ShippingInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsInsurance")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPriority")
                        .HasColumnType("bit");

                    b.Property<double>("ShippingPrice")
                        .HasColumnType("float");

                    b.Property<string>("TrackingNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("ShippingInfos");
                });

            modelBuilder.Entity("warehouse.Database.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashedPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("warehouse.Database.Entity.Items", b =>
                {
                    b.HasOne("warehouse.Database.Entity.IndexItem", "IndexItem")
                        .WithMany("Items")
                        .HasForeignKey("IndexItemId");

                    b.HasOne("warehouse.Database.Entity.User", "WhoCreated")
                        .WithMany("Items")
                        .HasForeignKey("WhoCreatedId");

                    b.Navigation("IndexItem");

                    b.Navigation("WhoCreated");
                });

            modelBuilder.Entity("warehouse.Database.Entity.Order", b =>
                {
                    b.HasOne("warehouse.Database.Entity.Client", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId");

                    b.HasOne("warehouse.Database.Entity.User", "WhoCreated")
                        .WithMany("Orders")
                        .HasForeignKey("WhoCreatedId");

                    b.Navigation("Client");

                    b.Navigation("WhoCreated");
                });

            modelBuilder.Entity("warehouse.Database.Entity.OrderDetails", b =>
                {
                    b.HasOne("warehouse.Database.Entity.Items", "Items")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ItemsId");

                    b.HasOne("warehouse.Database.Entity.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId");

                    b.Navigation("Items");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("warehouse.Database.Entity.ShippingInfo", b =>
                {
                    b.HasOne("warehouse.Database.Entity.Client", "Client")
                        .WithMany("ShippingInfo")
                        .HasForeignKey("ClientId");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("warehouse.Database.Entity.User", b =>
                {
                    b.HasOne("warehouse.Database.Entity.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("warehouse.Database.Entity.Client", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("ShippingInfo");
                });

            modelBuilder.Entity("warehouse.Database.Entity.IndexItem", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("warehouse.Database.Entity.Items", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("warehouse.Database.Entity.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("warehouse.Database.Entity.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("warehouse.Database.Entity.User", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
