﻿// <auto-generated />
using System;
using CRUDRecipeEF.BL.DL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CRUDRecipeEF.BL.DL.Migrations
{
    [DbContext(typeof(RecipeContext))]
    [Migration("20210314121709_CreateMenuRestaurant")]
    partial class CreateMenuRestaurant
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("CRUDRecipeEF.BL.DL.Entities.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("CRUDRecipeEF.BL.DL.Entities.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("RestaurantId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("CRUDRecipeEF.BL.DL.Entities.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MenuId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("MenuId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("CRUDRecipeEF.BL.DL.Entities.RecipeCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("RecipeCategories");
                });

            modelBuilder.Entity("CRUDRecipeEF.BL.DL.Entities.Restaurant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("IngredientRecipe", b =>
                {
                    b.Property<int>("IngredientsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RecipesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("IngredientsId", "RecipesId");

                    b.HasIndex("RecipesId");

                    b.ToTable("IngredientRecipe");
                });

            modelBuilder.Entity("CRUDRecipeEF.BL.DL.Entities.Menu", b =>
                {
                    b.HasOne("CRUDRecipeEF.BL.DL.Entities.Restaurant", null)
                        .WithMany("Menus")
                        .HasForeignKey("RestaurantId");
                });

            modelBuilder.Entity("CRUDRecipeEF.BL.DL.Entities.Recipe", b =>
                {
                    b.HasOne("CRUDRecipeEF.BL.DL.Entities.RecipeCategory", "Category")
                        .WithMany("Recipes")
                        .HasForeignKey("CategoryId");

                    b.HasOne("CRUDRecipeEF.BL.DL.Entities.Menu", null)
                        .WithMany("Recipes")
                        .HasForeignKey("MenuId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("IngredientRecipe", b =>
                {
                    b.HasOne("CRUDRecipeEF.BL.DL.Entities.Ingredient", null)
                        .WithMany()
                        .HasForeignKey("IngredientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CRUDRecipeEF.BL.DL.Entities.Recipe", null)
                        .WithMany()
                        .HasForeignKey("RecipesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CRUDRecipeEF.BL.DL.Entities.Menu", b =>
                {
                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("CRUDRecipeEF.BL.DL.Entities.RecipeCategory", b =>
                {
                    b.Navigation("Recipes");
                });

            modelBuilder.Entity("CRUDRecipeEF.BL.DL.Entities.Restaurant", b =>
                {
                    b.Navigation("Menus");
                });
#pragma warning restore 612, 618
        }
    }
}
