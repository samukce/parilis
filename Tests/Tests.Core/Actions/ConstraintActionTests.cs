﻿using Core.Actions;
using Core.Descriptions;
using Core.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Core.Actions {
    [TestFixture]
    public class ConstraintActionTests : MockTest {
        [TestFixtureSetUp]
        public override void InitializeClass() {
            base.InitializeClass();
            Mock<IConstraint>();
        }

        [Test]
        public void DefaultCreationMustCallConstraintsCreateMethod() {
            var defaultDescription = new DefaultDescription { Name = "test_name" };
            var defaultCreation = new DefaultCreation(ConnectionInfo, defaultDescription);

            defaultCreation.Execute();

            defaultCreation.Constraints.Received(1).CreateDefault(Arg.Is<DefaultDescription>(
                d => d.Name.Equals(defaultDescription.Name)));
        }

        [Test]
        public void DefaultRemovalMustCallConstraintsRemoveMethod() {
            var defaultDescription = new DefaultDescription {Name = "test_name"};
            var defaultRemoval = new DefaultRemoval(ConnectionInfo, defaultDescription);

            defaultRemoval.Execute();

            defaultRemoval.Constraints.Received(1).RemoveDefault(Arg.Is<DefaultDescription>(
                d => d.Name.Equals(defaultDescription.Name)));
        }

        [Test]
        public void UniqueCreationMustCallConstraintsCreateMethod() {
            var uniqueDescription = new UniqueDescription {Name = "test_name"};
            var uniqueCreation = new UniqueCreation(ConnectionInfo, uniqueDescription);

            uniqueCreation.Execute();

            uniqueCreation.Constraints.Received(1).CreateUnique(Arg.Is<UniqueDescription>(
                d => d.Name.Equals(uniqueDescription.Name)));
        }

        [Test]
        public void UniqueRemovalMustCallConstraintsRemoveMethod() {
            var uniqueDescription = new UniqueDescription { Name = "test_name" };
            var uniqueRemoval = new UniqueRemoval(ConnectionInfo, uniqueDescription);

            uniqueRemoval.Execute();

            uniqueRemoval.Constraints.Received(1).RemoveUnique(Arg.Is<UniqueDescription>(
                d => d.Name.Equals(uniqueDescription.Name)));
        }

        [Test]
        public void ForeignKeyCreationMustCallConstraintsCreateMethod() {
            var foreignKeyDescription = new ForeignKeyDescription { Name = "test_name" };
            var uniqueCreation = new ForeignKeyCreation(ConnectionInfo, foreignKeyDescription);

            uniqueCreation.Execute();

            uniqueCreation.Constraints.Received(1).CreateForeignKey(Arg.Is<ForeignKeyDescription>(
                d => d.Name.Equals(foreignKeyDescription.Name)));
        }

        [Test]
        public void ForeignKeyRemovalMustCallConstraintsRemoveMethod() {
            var foreignKeyDescription = new ForeignKeyDescription { Name = "test_name" };
            var uniqueCreation = new ForeignKeyRemoval(ConnectionInfo, foreignKeyDescription);

            uniqueCreation.Execute();

            uniqueCreation.Constraints.Received(1).RemoveForeignKey(Arg.Is<ForeignKeyDescription>(
                d => d.Name.Equals(foreignKeyDescription.Name)));
        }

        [Test]
        public void PrimaryKeyCreationMustCallConstraintsCreateMethod() {
            var primaryKeyDescription = new PrimaryKeyDescription { Name = "test_name" };
            var uniqueCreation = new PrimaryKeyCreation(ConnectionInfo, primaryKeyDescription);

            uniqueCreation.Execute();

            uniqueCreation.Constraints.Received(1).CreatePrimaryKey(Arg.Is<PrimaryKeyDescription>(
                d => d.Name.Equals(primaryKeyDescription.Name)));
        }

        [Test]
        public void PrimaryKeyRemovalMustCallConstraintsRemoveMethod() {
            var primaryKeyDescription = new PrimaryKeyDescription { Name = "test_name" };
            var uniqueCreation = new PrimaryKeyRemoval(ConnectionInfo, primaryKeyDescription);

            uniqueCreation.Execute();

            uniqueCreation.Constraints.Received(1).RemovePrimaryKey(Arg.Is<PrimaryKeyDescription>(
                d => d.Name.Equals(primaryKeyDescription.Name)));
        }
    }
}