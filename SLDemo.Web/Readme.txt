https://msdn.microsoft.com/fr-fr/library/x28wfk74(v=vs.100).aspx
C:\WINDOWS\Microsoft.NET\Framework\<versionNumber>\aspnet_regsql.exe


http://stackoverflow.com/questions/30038999/differences-between-commit-commit-and-push-commit-and-sync
https://jeremybytes.blogspot.fr/2014/12/getting-used-to-git-in-visual-studio.html
https://www.youtube.com/watch?v=H0OcllB2IwA&feature=youtu.be

Differences between Commit, Commit and Push, Commit and Sync
Option 1 says Commit
Option 2 says Commit and Push
Option 3 says Commit and Sync

1.Commit will simply make record of your changes that you have made on your local machine. It will not mark the change in the remote repository.
2.Commit and Push will do the above and push it to the remote repository. This means that any changes you have made will be saved to the remote repository as well.
3.Commit and Sync does three things. First, it will commit. Second, it will perform a pull (grabs the updated information from the remote repo). Finally, it will push.