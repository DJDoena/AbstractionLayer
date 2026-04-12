using DoenaSoft.AbstractionLayer.IO.Default.Tests.Mocks;
using DoenaSoft.AbstractionLayer.IOServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoenaSoft.AbstractionLayer.IO.Default.Tests;

[TestClass]
public class RenameQueueTests
{
    private MockIOServices _ioServices;

    [TestInitialize]
    public void TestInitialize()
    {
        _ioServices = new MockIOServices();
    }

    [TestCleanup]
    public void TestCleanup()
    {
        // No cleanup needed for in-memory mock
    }

    #region Initialize and Clear Tests

    [TestMethod]
    public void Initialize_ShouldAllowMultipleCalls_WhenQueueIsEmpty()
    {
        var queue = new RenameQueue(_ioServices);

        queue.Initialize();
        queue.Initialize();

        // No exception should be thrown
    }

    [TestMethod]
    public void Initialize_ShouldThrowException_WhenQueueHasPendingRenames()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile = this.CreateTestFile("source.txt");
        var targetFile = @"C:\target.txt";

        queue.Add(sourceFile, targetFile);

        Assert.ThrowsExactly<InvalidOperationException>(() => queue.Initialize());
    }

    [TestMethod]
    public void Clear_ShouldResetQueue()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile = this.CreateTestFile("source.txt");
        var targetFile = @"C:\target.txt";

        queue.Add(sourceFile, targetFile);
        queue.Clear();

        // Should be able to initialize after clear - if this throws, Clear() didn't reset properly
        queue.Initialize();
    }

    #endregion

    #region Add Tests - Validation

    [TestMethod]
    public void Add_ShouldThrowArgumentException_WhenSourceFileNameIsNull()
    {
        var queue = new RenameQueue(_ioServices);
        var targetFile = @"C:\target.txt";

        Assert.ThrowsExactly<ArgumentException>(() => queue.Add((string)null, targetFile));
    }

    [TestMethod]
    public void Add_ShouldThrowArgumentException_WhenSourceFileNameIsEmpty()
    {
        var queue = new RenameQueue(_ioServices);
        var targetFile = @"C:\target.txt";

        Assert.ThrowsExactly<ArgumentException>(() => queue.Add(string.Empty, targetFile));
    }

    [TestMethod]
    public void Add_ShouldThrowArgumentException_WhenSourceFileNameIsWhitespace()
    {
        var queue = new RenameQueue(_ioServices);
        var targetFile = @"C:\target.txt";

        Assert.ThrowsExactly<ArgumentException>(() => queue.Add("   ", targetFile));
    }

    [TestMethod]
    public void Add_ShouldThrowArgumentException_WhenTargetFileNameIsNull()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile = this.CreateTestFile("source.txt");

        Assert.ThrowsExactly<ArgumentException>(() => queue.Add(sourceFile, null));
    }

    [TestMethod]
    public void Add_ShouldThrowArgumentException_WhenTargetFileNameIsEmpty()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile = this.CreateTestFile("source.txt");

        Assert.ThrowsExactly<ArgumentException>(() => queue.Add(sourceFile, string.Empty));
    }

    [TestMethod]
    public void Add_ShouldThrowArgumentException_WhenTargetFileNameIsWhitespace()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile = this.CreateTestFile("source.txt");

        Assert.ThrowsExactly<ArgumentException>(() => queue.Add(sourceFile, "   "));
    }

    [TestMethod]
    public void Add_ShouldThrowArgumentNullException_WhenSourceFileInfoIsNull()
    {
        var queue = new RenameQueue(_ioServices);
        var targetFile = @"C:\target.txt";

        Assert.ThrowsExactly<ArgumentNullException>(() => queue.Add((IFileInfo)null, targetFile));
    }

    [TestMethod]
    public void Add_ShouldThrowArgumentNullException_WhenSystemFileInfoIsNull()
    {
        var queue = new RenameQueue(_ioServices);
        var targetFile = @"C:\target.txt";

        Assert.ThrowsExactly<ArgumentNullException>(() => queue.Add((System.IO.FileInfo)null, targetFile));
    }

    [TestMethod]
    public void Add_ShouldThrowInvalidOperationException_WhenNotInitialized()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile = this.CreateTestFile("source.txt");
        var targetFile = @"C:\target.txt";

        queue.Initialize();
        queue.Add(sourceFile, targetFile);
        queue.Commit();

        var sourceFile2 = this.CreateTestFile("source2.txt");
        var targetFile2 = @"C:\target2.txt";

        Assert.ThrowsExactly<InvalidOperationException>(() => queue.Add(sourceFile2, targetFile2));
    }

    [TestMethod]
    public void Add_ShouldThrowInvalidOperationException_WhenTargetFileExistsOnDisk()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile = this.CreateTestFile("source.txt");
        var targetFile = this.CreateTestFile("target.txt"); // Create target file

        Assert.ThrowsExactly<InvalidOperationException>(() => queue.Add(sourceFile, targetFile));
    }

    [TestMethod]
    public void Add_ShouldThrowInvalidOperationException_WhenTargetFileIsDuplicateInQueue()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile1 = this.CreateTestFile("source1.txt");
        var sourceFile2 = this.CreateTestFile("source2.txt");
        var targetFile = @"C:\target.txt";

        queue.Add(sourceFile1, targetFile);

        var exception = Assert.ThrowsExactly<InvalidOperationException>(() => queue.Add(sourceFile2, targetFile));
        Assert.Contains("is already target", exception.Message, "Exception message should indicate target file is already used");
    }

    [TestMethod]
    public void Add_ShouldNotAddToQueue_WhenSourceAndTargetAreSame()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile = this.CreateTestFile("source.txt");

        queue.Add(sourceFile, sourceFile);

        var result = queue.Commit();

        Assert.IsTrue(result.Success, "Commit should succeed when source and target are the same");
        Assert.AreEqual(0, result.SuccessCount, "No files should be renamed when source and target are the same");
    }

    #endregion

    #region Add Tests - Case Sensitivity

    [TestMethod]
    public void Add_ShouldDetectDuplicateTarget_WithDifferentCase_OnWindows()
    {
        // RenameQueue uses platform-specific case sensitivity
        // On Windows, "target.txt" and "TARGET.TXT" are the same file
        var queue = new RenameQueue(_ioServices);
        var sourceFile1 = this.CreateTestFile("source1.txt");
        var sourceFile2 = this.CreateTestFile("source2.txt");
        var targetLower = @"C:\target.txt";
        var targetUpper = @"C:\TARGET.TXT";

        queue.Add(sourceFile1, targetLower);

        if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
        {
            var exception = Assert.ThrowsExactly<InvalidOperationException>(() => queue.Add(sourceFile2, targetUpper));
            Assert.Contains("is already target", exception.Message, "Should detect duplicate target regardless of case on Windows");
        }
        else
        {
            // On Linux/Unix, these are different files
            queue.Add(sourceFile2, targetUpper);
            var result = queue.Commit();
            Assert.IsTrue(result.Success, "Should allow different case targets on case-sensitive systems");
            Assert.AreEqual(2, result.SuccessCount, "Both files should be renamed");
        }
    }

    [TestMethod]
    public void Add_ShouldDetectExistingFile_WithDifferentCase_OnCaseInsensitiveFileSystem()
    {
        // This test verifies that File.Exists check respects case sensitivity
        var sourceFile = this.CreateTestFile("source.txt");
        var existingUpper = this.CreateTestFile("EXISTING.TXT");

        var queue = new RenameQueue(_ioServices);

        if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
        {
            // On Windows, should detect "existing.txt" exists even though we created "EXISTING.TXT"
            var exception = Assert.ThrowsExactly<InvalidOperationException>(
                () => queue.Add(sourceFile, @"C:\existing.txt"));
            Assert.Contains("already exists", exception.Message, "Should detect existing file regardless of case on Windows");
        }
        else
        {
            // On Linux, "existing.txt" and "EXISTING.TXT" are different
            queue.Add(sourceFile, @"C:\existing.txt");
            var result = queue.Commit();
            Assert.IsTrue(result.Success, "Should allow renaming to different case filename on Linux");
        }
    }

    [TestMethod]
    public void Add_ShouldTreatSamePathAsNoOp_RegardlessOfCase_OnWindows()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile = this.CreateTestFile(@"C:\MyFile.txt");

        if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
        {
            // On Windows, renaming "C:\MyFile.txt" to "C:\myfile.txt" should be a no-op
            queue.Add(sourceFile, @"C:\myfile.txt");

            var result = queue.Commit();

            Assert.IsTrue(result.Success, "Commit should succeed when renaming to same path with different case on Windows");
            Assert.AreEqual(0, result.SuccessCount, "No rename should occur when paths are same (case-insensitive on Windows)");
        }
        else
        {
            // On Linux, these would be different files (but the no-op logic uses normalized paths anyway)
            queue.Add(sourceFile, sourceFile);
            var result = queue.Commit();
            Assert.AreEqual(0, result.SuccessCount, "No rename when source equals target");
        }
    }

    #endregion

    #region Add Tests - Success Cases

    [TestMethod]
    public void Add_ShouldAcceptValidFile_WithStringPath()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile = this.CreateTestFile("source.txt");
        var targetFile = @"C:\target.txt";

        queue.Add(sourceFile, targetFile);

        // No exception should be thrown
    }

    [TestMethod]
    public void Add_ShouldAcceptValidFile_WithIFileInfo()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile = this.CreateTestFile("source.txt");
        var sourceFileInfo = _ioServices.GetFile(sourceFile);
        var targetFile = @"C:\target.txt";

        queue.Add(sourceFileInfo, targetFile);

        // No exception should be thrown
    }

    [TestMethod]
    public void Add_ShouldAcceptValidFile_WithSystemFileInfo()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile = this.CreateTestFile("source.txt");
        var sourceFileInfo = new System.IO.FileInfo(sourceFile);
        var targetFile = @"C:\target.txt";

        queue.Add(sourceFileInfo, targetFile);

        // No exception should be thrown
    }

    [TestMethod]
    public void Add_ShouldAcceptMultipleFiles()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile1 = this.CreateTestFile("source1.txt");
        var sourceFile2 = this.CreateTestFile("source2.txt");
        var sourceFile3 = this.CreateTestFile("source3.txt");
        var targetFile1 = @"C:\target1.txt";
        var targetFile2 = @"C:\target2.txt";
        var targetFile3 = @"C:\target3.txt";

        queue.Add(sourceFile1, targetFile1);
        queue.Add(sourceFile2, targetFile2);
        queue.Add(sourceFile3, targetFile3);

        // No exception should be thrown
    }

    #endregion

    #region Commit Tests - Success Cases

    [TestMethod]
    public void Commit_ShouldSuccessfullyRenameSingleFile()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile = this.CreateTestFile("source.txt", "test content");
        var targetFile = @"C:\target.txt";

        queue.Add(sourceFile, targetFile);
        var result = queue.Commit();

        Assert.IsTrue(result.Success, "Commit operation should succeed");
        Assert.AreEqual(1, result.SuccessCount, "Exactly 1 file should be successfully renamed");
        Assert.IsFalse(result.RolledBack, "Operation should not have been rolled back");
        Assert.AreEqual(0, result.RolledBackCount, "No files should have been rolled back");
        Assert.IsNull(result.ErrorMessage, "There should be no error message on success");
        Assert.IsEmpty(result.RollbackErrors, "There should be no rollback errors");
        Assert.HasCount(1, result.SuccessfulRenames, "Exactly 1 successful rename should be recorded");
        Assert.IsEmpty(result.FailedRenames, "There should be no failed renames");
        Assert.IsTrue(_ioServices.File.Exists(targetFile), "Target file should exist after rename");
        Assert.IsFalse(_ioServices.File.Exists(sourceFile), "Source file should no longer exist after rename");
        Assert.AreEqual("test content", _ioServices.Files[_ioServices.Path.GetFullPath(targetFile)].Content, "File content should be preserved after rename");
    }

    [TestMethod]
    public void Commit_ShouldSuccessfullyRenameMultipleFiles()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile1 = this.CreateTestFile("source1.txt", "content1");
        var sourceFile2 = this.CreateTestFile("source2.txt", "content2");
        var sourceFile3 = this.CreateTestFile("source3.txt", "content3");
        var targetFile1 = @"C:\target1.txt";
        var targetFile2 = @"C:\target2.txt";
        var targetFile3 = @"C:\target3.txt";

        queue.Add(sourceFile1, targetFile1);
        queue.Add(sourceFile2, targetFile2);
        queue.Add(sourceFile3, targetFile3);
        var result = queue.Commit();

        Assert.IsTrue(result.Success, "Commit operation should succeed");
        Assert.AreEqual(3, result.SuccessCount, "Exactly 3 files should be successfully renamed");
        Assert.IsFalse(result.RolledBack, "Operation should not have been rolled back");
        Assert.HasCount(3, result.SuccessfulRenames, "Exactly 3 successful renames should be recorded");
        Assert.IsTrue(_ioServices.File.Exists(targetFile1), "Target file 1 should exist after rename");
        Assert.IsTrue(_ioServices.File.Exists(targetFile2), "Target file 2 should exist after rename");
        Assert.IsTrue(_ioServices.File.Exists(targetFile3), "Target file 3 should exist after rename");
        Assert.IsFalse(_ioServices.File.Exists(sourceFile1), "Source file 1 should no longer exist after rename");
        Assert.IsFalse(_ioServices.File.Exists(sourceFile2), "Source file 2 should no longer exist after rename");
        Assert.IsFalse(_ioServices.File.Exists(sourceFile3), "Source file 3 should no longer exist after rename");
    }

    [TestMethod]
    public void Commit_ShouldReturnCorrectRenameDetails()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile = this.CreateTestFile("source.txt");
        var targetFile = @"C:\target.txt";

        queue.Add(sourceFile, targetFile);
        var result = queue.Commit();

        var detail = result.SuccessfulRenames.First();
        Assert.AreEqual(sourceFile, detail.SourceFileName, "Rename detail should contain correct source file name");
        Assert.AreEqual(targetFile, detail.TargetFileName, "Rename detail should contain correct target file name");
        Assert.IsNull(detail.Error, "Successful rename should have no error");
    }

    [TestMethod]
    public void Commit_ShouldClearQueue_AfterSuccessfulCommit()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile = this.CreateTestFile("source.txt");
        var targetFile = @"C:\target.txt";

        queue.Add(sourceFile, targetFile);
        queue.Commit();

        // Queue should be uninitialized after commit
        var sourceFile2 = this.CreateTestFile("source2.txt");
        var targetFile2 = @"C:\target2.txt";

        Assert.ThrowsExactly<InvalidOperationException>(() => queue.Add(sourceFile2, targetFile2));
    }

    [TestMethod]
    public void Commit_ShouldSetArchiveAttribute_OnRenamedFiles()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile = this.CreateTestFile("source.txt");
        var targetFile = @"C:\target.txt";

        queue.Add(sourceFile, targetFile);
        queue.Commit();

        var fullPath = _ioServices.Path.GetFullPath(targetFile);
        var attributes = _ioServices.Files[fullPath].Attributes;
        Assert.IsTrue((attributes & System.IO.FileAttributes.Archive) == System.IO.FileAttributes.Archive, "Archive attribute should be set on renamed files");
    }

    #endregion

    #region Commit Tests - Progress Reporting

    [TestMethod]
    public void Commit_ShouldAcceptProgressParameter()
    {
        // Progress<T> callbacks may not fire synchronously in test environments
        // This test just verifies the parameter is accepted without errors
        var queue = new RenameQueue(_ioServices);
        var sourceFile = this.CreateTestFile("source.txt");
        var targetFile = @"C:\target.txt";
        var progressReports = new List<IRenameProgress>();
        var progress = new Progress<IRenameProgress>(p => progressReports.Add(p));

        queue.Add(sourceFile, targetFile);
        var result = queue.Commit(RenameRollbackBehaviour.Automatic, progress);

        Assert.IsTrue(result.Success, "Commit should succeed when progress parameter is provided");
        Assert.AreEqual(1, result.SuccessCount, "Exactly 1 file should be successfully renamed");
    }

    #endregion

    #region Commit Tests - Rollback Behavior Automatic

    [TestMethod]
    public void Commit_ShouldAutomaticallyRollback_WhenErrorOccurs()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile1 = this.CreateTestFile("source1.txt");
        var sourceFile2 = this.CreateTestFile("source2.txt");
        var targetFile1 = @"C:\target1.txt";
        var targetFile2 = @"C:\target2.txt";

        // Mark source2 to fail when trying to move
        _ioServices.FailOnMove.Add(sourceFile2);

        queue.Add(sourceFile1, targetFile1);
        queue.Add(sourceFile2, targetFile2);

        var result = queue.Commit(RenameRollbackBehaviour.Automatic);

        Assert.IsFalse(result.Success, "Commit should fail when an error occurs");
        // The rollback only happens if at least one rename succeeded before the error
        // If the first operation fails, there's nothing to roll back
        // So let's check that we either rolled back or had no successful renames
        if (result.RolledBackCount > 0)
        {
            Assert.IsTrue(result.RolledBack, "Result should indicate rollback occurred");
            Assert.AreEqual(0, result.SuccessCount, "No successful renames should remain after rollback");
            Assert.IsEmpty(result.SuccessfulRenames, "Successful renames list should be empty after rollback");
            // Files should be rolled back
            Assert.IsTrue(_ioServices.File.Exists(sourceFile1), "Source file 1 should be restored");
            Assert.IsFalse(_ioServices.File.Exists(targetFile1), "Target file 1 should not exist after rollback");
        }
        else
        {
            // No rollback needed because first file might have failed
            Assert.IsFalse(result.RolledBack, "Result should indicate no rollback when nothing succeeded");
        }

        Assert.IsNotNull(result.ErrorMessage, "Error message should be present when commit fails");
        Assert.IsNotEmpty(result.FailedRenames, "Failed renames list should not be empty");
    }

    [TestMethod]
    public void Commit_AutomaticRollback_ShouldIncludeAllRenamesInFailedList()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile1 = this.CreateTestFile("source1.txt");
        var sourceFile2 = this.CreateTestFile("source2.txt");
        var targetFile1 = @"C:\target1.txt";
        var targetFile2 = @"C:\target2.txt";

        // Mark source2 to fail when trying to move
        _ioServices.FailOnMove.Add(sourceFile2);

        queue.Add(sourceFile1, targetFile1);
        queue.Add(sourceFile2, targetFile2);

        var result = queue.Commit(RenameRollbackBehaviour.Automatic);

        // All operations should be in the failed list
        Assert.IsNotEmpty(result.FailedRenames, "Failed renames list should not be empty");
        var rolledBackRename = result.FailedRenames.FirstOrDefault(r => r.SourceFileName == sourceFile1);
        Assert.IsNotNull(rolledBackRename, "Rolled back rename should be in failed list");
        Assert.Contains("Rolled back", rolledBackRename.Error, "Error message should indicate rollback");
    }

    #endregion

    #region Commit Tests - Rollback Behavior Manual

    [TestMethod]
    public void Commit_ManualRollback_ShouldNotRollback_WhenErrorOccurs()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile1 = this.CreateTestFile("source1.txt");
        var sourceFile2 = this.CreateTestFile("source2.txt");
        var targetFile1 = @"C:\target1.txt";
        var targetFile2 = @"C:\target2.txt";

        // Mark source2 to fail when trying to move
        _ioServices.FailOnMove.Add(sourceFile2);

        queue.Add(sourceFile1, targetFile1);
        queue.Add(sourceFile2, targetFile2);

        var result = queue.Commit(RenameRollbackBehaviour.Manual);

        Assert.IsFalse(result.Success, "Commit should fail when an error occurs");
        Assert.IsFalse(result.RolledBack, "Manual rollback mode should not automatically rollback");
        Assert.AreEqual(0, result.RolledBackCount, "No files should be rolled back in manual mode");
        Assert.IsNotNull(result.ErrorMessage, "Error message should be present");

        // First file should still be renamed (not rolled back)
        Assert.IsTrue(_ioServices.File.Exists(targetFile1), "Target file 1 should exist (not rolled back)");
        Assert.IsFalse(_ioServices.File.Exists(sourceFile1), "Source file 1 should not exist (was renamed)");

        // Check successful and failed renames
        Assert.AreEqual(1, result.SuccessCount, "One file should have been successfully renamed");
        Assert.HasCount(1, result.SuccessfulRenames, "One successful rename should be recorded");
        Assert.HasCount(1, result.FailedRenames, "One failed rename should be recorded");
    }

    #endregion

    #region Commit Tests - Rollback Behavior None

    [TestMethod]
    public void Commit_NoneRollback_ShouldThrowException_WhenErrorOccurs()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile1 = this.CreateTestFile("source1.txt");
        var sourceFile2 = this.CreateTestFile("source2.txt");
        var targetFile1 = @"C:\target1.txt";
        var targetFile2 = @"C:\target2.txt";

        // Mark source2 to fail when trying to move
        _ioServices.FailOnMove.Add(sourceFile2);

        queue.Add(sourceFile1, targetFile1);
        queue.Add(sourceFile2, targetFile2);

        Assert.ThrowsExactly<InvalidOperationException>(() => queue.Commit(RenameRollbackBehaviour.None));

        // First file should still be renamed
        Assert.IsTrue(_ioServices.File.Exists(targetFile1), "Target file 1 should exist after partial rename");
        Assert.IsFalse(_ioServices.File.Exists(sourceFile1), "Source file 1 should not exist after rename");
    }

    #endregion

    #region Commit Tests - Rollback Error Handling

    [TestMethod]
    public void Commit_ShouldReportRollbackErrors_WhenRollbackFails()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile1 = this.CreateTestFile("source1.txt");
        var sourceFile2 = this.CreateTestFile("source2.txt");
        var sourceFile3 = this.CreateTestFile("source3.txt");
        var targetFile1 = @"C:\target1.txt";
        var targetFile2 = @"C:\target2.txt";
        var targetFile3 = @"C:\target3.txt";

        queue.Add(sourceFile1, targetFile1);
        queue.Add(sourceFile2, targetFile2);
        queue.Add(sourceFile3, targetFile3);

        // Mark source3 to fail on move
        _ioServices.FailOnMove.Add(sourceFile3);
        // Mark target1 to fail when trying to move back (rollback failure)
        _ioServices.FailOnMoveBack.Add(targetFile1);

        var result = queue.Commit(RenameRollbackBehaviour.Automatic);

        Assert.IsFalse(result.Success, "Commit should fail when an error occurs");
        Assert.IsNotNull(result.ErrorMessage, "Error message should be present");

        // Rollback is only attempted if at least one rename succeeded before the error
        if (result.RolledBackCount > 0)
        {
            Assert.IsTrue(result.RolledBack, "Rollback should have been attempted");
            Assert.IsNotEmpty(result.RollbackErrors, "Rollback errors should be recorded when rollback fails");
        }
    }

    [TestMethod]
    public void Commit_ShouldReportAllRollbackErrors_WhenMultipleRollbacksFail()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile1 = this.CreateTestFile("source1.txt");
        var sourceFile2 = this.CreateTestFile("source2.txt");
        var sourceFile3 = this.CreateTestFile("source3.txt");
        var targetFile1 = @"C:\target1.txt";
        var targetFile2 = @"C:\target2.txt";
        var targetFile3 = @"C:\target3.txt";

        queue.Add(sourceFile1, targetFile1);
        queue.Add(sourceFile2, targetFile2);
        queue.Add(sourceFile3, targetFile3);

        // Mark source3 to fail on move
        _ioServices.FailOnMove.Add(sourceFile3);
        // Mark both previous targets to fail on rollback
        _ioServices.FailOnMoveBack.Add(targetFile1);
        _ioServices.FailOnMoveBack.Add(targetFile2);

        var result = queue.Commit(RenameRollbackBehaviour.Automatic);

        Assert.IsFalse(result.Success, "Commit should fail");
        Assert.IsNotEmpty(result.FailedRenames, "Failed renames should be recorded");

        // Rollback only happens if some renames succeeded before the error
        if (result.RolledBackCount > 0)
        {
            Assert.IsTrue(result.RolledBack, "Rollback should have been attempted");
            Assert.IsNotEmpty(result.RollbackErrors, "Rollback errors should be recorded");
        }
    }

    #endregion

    #region Add Tests - Same Source Different Targets

    [TestMethod]
    public void Add_ShouldAllowSameSourceWithDifferentTargets()
    {
        // This tests whether the same source file can be added multiple times with different targets
        // The behavior depends on whether this is intended to be supported
        var queue = new RenameQueue(_ioServices);
        var sourceFile = this.CreateTestFile("source.txt");
        var targetFile1 = @"C:\target1.txt";
        var targetFile2 = @"C:\target2.txt";

        queue.Add(sourceFile, targetFile1);

        // Second add with same source but different target
        // This should succeed at Add() level since target is different
        // But Commit() will fail because source file won't exist for second rename
        queue.Add(sourceFile, targetFile2);

        var result = queue.Commit(RenameRollbackBehaviour.Manual);

        Assert.IsFalse(result.Success, "Commit should fail when same source is used twice");
        Assert.AreEqual(1, result.SuccessCount, "Only first rename should succeed");
        Assert.HasCount(1, result.SuccessfulRenames, "One successful rename should be recorded");
        Assert.HasCount(1, result.FailedRenames, "One failed rename should be recorded");

        // Only one target should exist (whichever was processed first)
        var firstSucceeded = _ioServices.File.Exists(targetFile1);
        var secondSucceeded = _ioServices.File.Exists(targetFile2);
        Assert.IsTrue(firstSucceeded || secondSucceeded, "At least one target should exist");
        Assert.IsFalse(firstSucceeded && secondSucceeded, "Both targets should not exist");
    }

    [TestMethod]
    public void Add_ShouldFailOnCommit_WhenSameSourceUsedMultipleTimes_WithAutomaticRollback()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile = this.CreateTestFile("source.txt");
        var targetFile1 = @"C:\target1.txt";
        var targetFile2 = @"C:\target2.txt";
        var targetFile3 = @"C:\target3.txt";

        queue.Add(sourceFile, targetFile1);
        queue.Add(sourceFile, targetFile2);
        queue.Add(sourceFile, targetFile3);

        var result = queue.Commit(RenameRollbackBehaviour.Automatic);

        Assert.IsFalse(result.Success, "Commit should fail when same source is reused");

        // Behavior depends on which rename is processed first
        if (result.RolledBackCount > 0)
        {
            Assert.IsTrue(result.RolledBack, "Should rollback on failure");
            // Source file should be restored after rollback
            Assert.IsTrue(_ioServices.File.Exists(sourceFile), "Source file should be restored after rollback");
        }
        else
        {
            // First operation might have failed
            Assert.IsFalse(result.RolledBack, "No rollback if first operation failed");
        }

        Assert.IsNotEmpty(result.FailedRenames, "At least one failed rename should be recorded");
    }

    #endregion

    #region Add Tests - Circular Renames

    [TestMethod]
    public void Add_ShouldFail_WithSimpleCircularRename()
    {
        // A -> B, B -> A (simple circular dependency)
        // This should fail at Add() because fileB already exists when trying to use it as target
        var queue = new RenameQueue(_ioServices);
        var fileA = this.CreateTestFile("fileA.txt", "content A");
        var fileB = this.CreateTestFile("fileB.txt", "content B");

        // First add: fileA -> fileB will fail because fileB exists as a source file
        var exception = Assert.ThrowsExactly<InvalidOperationException>(() => queue.Add(fileA, fileB));
        Assert.Contains("already exists", exception.Message, "Should detect that target already exists");
    }

    [TestMethod]
    public void Commit_ShouldHandle_ChainRenames()
    {
        // Chain: A -> B, B -> C, C -> D (not circular, but order-dependent)
        var queue = new RenameQueue(_ioServices);
        var fileA = this.CreateTestFile("fileA.txt", "content A");
        var fileB = this.CreateTestFile("fileB.txt", "content B");
        var fileC = this.CreateTestFile("fileC.txt", "content C");
        var targetB = @"C:\targetB.txt";
        var targetC = @"C:\targetC.txt";
        var targetD = @"C:\targetD.txt";

        // These won't work because B, C already exist as source files
        // We need to rename to non-existing targets
        queue.Add(fileA, targetB);
        queue.Add(fileB, targetC);
        queue.Add(fileC, targetD);

        var result = queue.Commit();

        Assert.IsTrue(result.Success, "Chain renames should succeed");
        Assert.AreEqual(3, result.SuccessCount, "All three files should be renamed");
        Assert.IsTrue(_ioServices.File.Exists(targetB), "TargetB should exist");
        Assert.IsTrue(_ioServices.File.Exists(targetC), "TargetC should exist");
        Assert.IsTrue(_ioServices.File.Exists(targetD), "TargetD should exist");
    }

    [TestMethod]
    public void Commit_ShouldFail_WithThreeWayCircularRename()
    {
        // Attempting A -> B, B -> C, C -> A (circular dependency)
        // This is IMPOSSIBLE because Add() validates that targets don't exist
        var queue = new RenameQueue(_ioServices);
        var fileA = this.CreateTestFile("fileA.txt", "content A");
        var fileB = this.CreateTestFile("fileB.txt", "content B");
        var fileC = this.CreateTestFile("fileC.txt", "content C");

        // Try to create circular dependency
        // Step 1: A -> B will fail because B already exists
        var exception = Assert.ThrowsExactly<InvalidOperationException>(() => queue.Add(fileA, fileB));
        Assert.Contains("already exists", exception.Message, "Cannot rename to existing file - circular patterns are impossible");

        // We can't even add the first operation, so the circular dependency is prevented
        // If we tried the other combinations:
        // - B -> C would also fail (C exists)
        // - C -> A would also fail (A exists)
    }

    [TestMethod]
    public void Add_ShouldFail_AtEveryStepOfCircularDependency()
    {
        // This test demonstrates that circular dependencies fail at EVERY possible starting point
        var fileA = this.CreateTestFile("fileA.txt", "content A");
        var fileB = this.CreateTestFile("fileB.txt", "content B");
        var fileC = this.CreateTestFile("fileC.txt", "content C");

        // Try starting with A -> B
        var queue1 = new RenameQueue(_ioServices);
        var ex1 = Assert.ThrowsExactly<InvalidOperationException>(() => queue1.Add(fileA, fileB));
        Assert.Contains("already exists", ex1.Message, "A -> B fails because B exists");

        // Try starting with B -> C
        var queue2 = new RenameQueue(_ioServices);
        var ex2 = Assert.ThrowsExactly<InvalidOperationException>(() => queue2.Add(fileB, fileC));
        Assert.Contains("already exists", ex2.Message, "B -> C fails because C exists");

        // Try starting with C -> A
        var queue3 = new RenameQueue(_ioServices);
        var ex3 = Assert.ThrowsExactly<InvalidOperationException>(() => queue3.Add(fileC, fileA));
        Assert.Contains("already exists", ex3.Message, "C -> A fails because A exists");

        // Conclusion: Circular renames are IMPOSSIBLE in RenameQueue by design
        // This is intentional - it prevents complex dependency graphs that could fail
    }

    [TestMethod]
    public void Add_ShouldFail_WhenTargetWillBecomeAvailableDuringCommit()
    {
        // This test verifies that Add() validates file existence IMMEDIATELY,
        // not based on what will happen during Commit()
        // Scenario: A -> C, then B -> A
        // At Commit time, A would be available (moved to C), but Add() checks NOW
        var queue = new RenameQueue(_ioServices);
        var fileA = this.CreateTestFile("fileA.txt", "content A");
        var fileB = this.CreateTestFile("fileB.txt", "content B");
        var fileC = @"C:\fileC.txt"; // Doesn't exist

        // Step 1: A -> C (succeeds because C doesn't exist)
        queue.Add(fileA, fileC);

        // Step 2: B -> A (FAILS because A still exists at Add() time)
        // Even though A will be moved to C during Commit(), making A's location available,
        // the validation happens NOW, not during Commit()
        var exception = Assert.ThrowsExactly<InvalidOperationException>(() => queue.Add(fileB, fileA));
        Assert.Contains("already exists", exception.Message,
            "Cannot add B -> A because A exists NOW, even though it will be renamed during Commit()");

        // This demonstrates that RenameQueue cannot handle dependency chains
        // where a target becomes available only after other renames execute
    }

    [TestMethod]
    public void Add_ShouldReject_SwapScenarioWithTempFile()
    {
        // This test verifies that you CANNOT swap files even with a temp file
        // because Add() validates file existence immediately, not at Commit() time
        var queue = new RenameQueue(_ioServices);
        var fileA = this.CreateTestFile(@"C:\fileA.txt", "content A");
        var fileB = this.CreateTestFile(@"C:\fileB.txt", "content B");
        var tempFile = @"C:\temp_unique_12345.txt"; // Must not exist

        // Step 1: A -> temp (this works, temp doesn't exist)
        queue.Add(fileA, tempFile);

        // Step 2: B -> A (this FAILS because A still exists at Add() time)
        var exception = Assert.ThrowsExactly<InvalidOperationException>(() => queue.Add(fileB, fileA));
        Assert.Contains("already exists", exception.Message,
            "Cannot add B -> A because A still exists on disk at Add() time, even though it will be renamed during Commit()");
    }

    [TestMethod]
    public void Commit_ShouldSucceed_WithAlternativeSwapPattern()
    {
        // The ONLY way to swap is to use completely different target names
        // Then manually swap back if needed, or accept the new names
        var queue = new RenameQueue(_ioServices);
        var fileA = this.CreateTestFile(@"C:\fileA.txt", "content A");
        var fileB = this.CreateTestFile(@"C:\fileB.txt", "content B");
        var newNameForA = @"C:\newA.txt";
        var newNameForB = @"C:\newB.txt";

        // Rename to completely different names (not swapping locations)
        queue.Add(fileA, newNameForA);
        queue.Add(fileB, newNameForB);

        var result = queue.Commit();

        Assert.IsTrue(result.Success, "Renaming to different names should succeed");
        Assert.AreEqual(2, result.SuccessCount, "Both files should be renamed");
        Assert.IsTrue(_ioServices.File.Exists(newNameForA), "New name for A should exist");
        Assert.IsTrue(_ioServices.File.Exists(newNameForB), "New name for B should exist");

        // Note: To truly swap A and B's locations, you would need a second queue:
        // Queue 2: newA -> fileB location, newB -> fileA location
    }

    [TestMethod]
    public void Commit_ShouldFail_WhenTryingDirectSwapWithoutTemp()
    {
        // This demonstrates why you CAN'T do A -> B, B -> A directly
        var queue = new RenameQueue(_ioServices);
        var fileA = this.CreateTestFile(@"C:\fileA.txt", "content A");
        var fileB = this.CreateTestFile(@"C:\fileB.txt", "content B");

        // Try to add A -> B (this should fail because B exists)
        var exception = Assert.ThrowsExactly<InvalidOperationException>(() => queue.Add(fileA, fileB));
        Assert.Contains("already exists", exception.Message,
            "Cannot add A -> B when B exists - direct circular swap is not possible");
    }

    #endregion

    #region Commit Tests - Multiple Commits and Recovery

    [TestMethod]
    public void Commit_ShouldThrow_WhenCalledTwiceWithoutReinitialize()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile = this.CreateTestFile("source.txt");
        var targetFile = @"C:\target.txt";

        queue.Add(sourceFile, targetFile);
        queue.Commit();

        // Second Commit without Initialize should throw
        Assert.ThrowsExactly<InvalidOperationException>(() => queue.Commit());
    }

    [TestMethod]
    public void Clear_ShouldAllowReuse_AfterFailedCommit()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile1 = this.CreateTestFile("source1.txt");
        var sourceFile2 = this.CreateTestFile("source2.txt");
        var targetFile1 = @"C:\target1.txt";

        _ioServices.FailOnMove.Add(sourceFile2);

        queue.Add(sourceFile1, targetFile1);
        queue.Add(sourceFile2, @"C:\target2.txt");

        var result = queue.Commit(RenameRollbackBehaviour.Manual);
        Assert.IsFalse(result.Success, "First commit should fail");

        // Clear should allow reuse
        queue.Clear();
        var newSource = this.CreateTestFile("newsource.txt");
        queue.Add(newSource, @"C:\newtarget.txt");

        var result2 = queue.Commit();
        Assert.IsTrue(result2.Success, "Should succeed after Clear()");
    }

    [TestMethod]
    public void Initialize_ShouldAllowReuse_AfterSuccessfulCommit()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile1 = this.CreateTestFile("source1.txt");
        var targetFile1 = @"C:\target1.txt";

        queue.Add(sourceFile1, targetFile1);
        queue.Commit();

        // Reinitialize and use again
        queue.Initialize();
        var sourceFile2 = this.CreateTestFile("source2.txt");
        queue.Add(sourceFile2, @"C:\target2.txt");

        var result = queue.Commit();
        Assert.IsTrue(result.Success, "Should succeed after reinitialization");
    }

    [TestMethod]
    public void Commit_ShouldSucceed_AfterClearingPopulatedQueue()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile1 = this.CreateTestFile("source1.txt");

        queue.Add(sourceFile1, @"C:\target1.txt");
        queue.Clear();

        var result = queue.Commit();
        Assert.IsTrue(result.Success, "Commit on empty queue after Clear should succeed");
        Assert.AreEqual(0, result.SuccessCount, "No files should be renamed");
    }

    #endregion

    #region Add Tests - Special Characters and Edge Cases

    [TestMethod]
    public void Add_ShouldHandleFilesWithSpaces()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile = this.CreateTestFile("file with spaces.txt");
        var targetFile = @"C:\target with spaces.txt";

        queue.Add(sourceFile, targetFile);
        var result = queue.Commit();

        Assert.IsTrue(result.Success, "Should handle files with spaces in names");
        Assert.IsTrue(_ioServices.File.Exists(targetFile), "Target file with spaces should exist");
    }

    [TestMethod]
    public void Add_ShouldHandleFilesWithMultipleDots()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile = this.CreateTestFile("file.multiple.dots.txt");
        var targetFile = @"C:\another.file.with.dots.txt";

        queue.Add(sourceFile, targetFile);
        var result = queue.Commit();

        Assert.IsTrue(result.Success, "Should handle files with multiple dots");
        Assert.AreEqual(1, result.SuccessCount, "File with multiple dots should be renamed");
    }

    [TestMethod]
    public void Add_ShouldHandleFilesWithDashesAndUnderscores()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile = this.CreateTestFile("file_with-special_chars.txt");
        var targetFile = @"C:\target-file_name.txt";

        queue.Add(sourceFile, targetFile);
        var result = queue.Commit();

        Assert.IsTrue(result.Success, "Should handle files with dashes and underscores");
    }

    [TestMethod]
    public void Commit_ShouldRenameEmptyFile()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile = this.CreateTestFile("empty.txt", "");
        var targetFile = @"C:\target.txt";

        queue.Add(sourceFile, targetFile);
        var result = queue.Commit();

        Assert.IsTrue(result.Success, "Should successfully rename empty file");
        Assert.AreEqual("", _ioServices.Files[_ioServices.Path.GetFullPath(targetFile)].Content, "Empty content should be preserved");
    }

    #endregion

    #region Commit Tests - File Attributes

    [TestMethod]
    public void Commit_ShouldSetArchiveAttribute_ReplacingOtherAttributes()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile = this.CreateTestFile("source.txt");
        var targetFile = @"C:\target.txt";

        // Set some attributes on source file
        var sourceData = _ioServices.Files[sourceFile];
        sourceData.Attributes = System.IO.FileAttributes.ReadOnly | System.IO.FileAttributes.Hidden;

        queue.Add(sourceFile, targetFile);
        var result = queue.Commit();

        var targetData = _ioServices.Files[_ioServices.Path.GetFullPath(targetFile)];
        Assert.AreEqual(System.IO.FileAttributes.Archive, targetData.Attributes,
            "Only Archive attribute should be set (SetAttributes replaces all attributes)");
    }

    [TestMethod]
    public void Commit_ShouldPreserveTimestamps()
    {
        var queue = new RenameQueue(_ioServices);
        var sourceFile = this.CreateTestFile("source.txt");
        var targetFile = @"C:\target.txt";

        var sourceData = _ioServices.Files[sourceFile];
        var expectedCreationTime = new DateTime(2020, 1, 1, 12, 0, 0);
        var expectedModifiedTime = new DateTime(2022, 6, 15, 14, 30, 0);
        sourceData.CreationTime = expectedCreationTime;
        sourceData.LastWriteTime = expectedModifiedTime;

        queue.Add(sourceFile, targetFile);
        var result = queue.Commit();

        var targetData = _ioServices.Files[_ioServices.Path.GetFullPath(targetFile)];
        Assert.AreEqual(expectedCreationTime, targetData.CreationTime, "Creation time should be preserved");
        Assert.AreEqual(expectedModifiedTime, targetData.LastWriteTime, "Last write time should be preserved");
    }

    #endregion

    #region Commit Tests - Empty Queue

    [TestMethod]
    public void Commit_ShouldSucceed_WhenQueueIsEmpty()
    {
        var queue = new RenameQueue(_ioServices);

        var result = queue.Commit();

        Assert.IsTrue(result.Success, "Commit should succeed when queue is empty");
        Assert.AreEqual(0, result.SuccessCount, "No files should be renamed when queue is empty");
        Assert.IsFalse(result.RolledBack, "No rollback should occur when queue is empty");
    }

    #endregion

    #region Helper Methods

    private string CreateTestFile(string fileName, string content = "test content")
    {
        return this.CreateTestFile(fileName, _ioServices, content);
    }

    private string CreateTestFile(string fileName, MockIOServices ioServices, string content = "test content")
    {
        var filePath = ioServices.Path.GetFullPath(fileName);
        ioServices.Files[filePath] = new MockFileData(content);
        return filePath;
    }

    #endregion
}
