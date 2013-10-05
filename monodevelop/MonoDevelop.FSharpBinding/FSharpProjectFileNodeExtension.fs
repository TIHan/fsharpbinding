namespace MonoDevelop.FSharp

open System
open System.Diagnostics
open System.IO
open MonoDevelop.Core
open MonoDevelop.Components.Commands
open MonoDevelop.Ide.Gui.Components

open MonoDevelop.Projects
open MonoDevelop.Ide
open MonoDevelop.Ide.Gui

open System.Collections.Generic
open System.Linq
open System.Xml
open System.Xml.Linq


type FSharpProjectNodeCommandHandler() =
  inherit NodeCommandHandler()

  [<CommandHandler(MonoDevelop.FSharp.FSharpCommands.MoveUp)>]
  member x.MoveUp() = 
    //This never gets hit
    ()

  [<CommandUpdateHandler(MonoDevelop.FSharp.FSharpCommands.MoveUp)>]
  member x.OnUpdateMoveUp(cmd: CommandInfo) =
    //This never gets hit
    ()
   
  [<CommandHandler(MonoDevelop.FSharp.FSharpCommands.MoveDown)>]
  member x.MoveDown() =
    //This never gets hit
    ()

  [<CommandUpdateHandler(MonoDevelop.FSharp.FSharpCommands.MoveDown)>]
  member x.OnUpdateMoveDown(cmd: CommandInfo) =
  //This never gets hit
    ()

  override x.ActivateItem() =
    //This gets hit
    () 


type FSharpProjectFileNodeExtension() =
  inherit NodeBuilderExtension()

  override x.CanBuildNode(dataType:Type) =
    // Extend any file belonging to a F# project
    typedefof<ProjectFile>.IsAssignableFrom (dataType)

  override x.CompareObjects(thisNode:ITreeNavigator, otherNode:ITreeNavigator) : int =
    if otherNode.DataItem :? ProjectFile then
      let file1 = thisNode.DataItem :?> ProjectFile
      let file2 = otherNode.DataItem :?> ProjectFile
      if (file1.Project <> null) && (file1.Project = file2.Project) && (file1.Project :? DotNetProject) && ((file1.Project :?> DotNetProject).LanguageName = "F#") then
        file1.Project.Files.IndexOf(file1).CompareTo(file2.Project.Files.IndexOf(file2))
      else
        NodeBuilder.DefaultSort
    else
      NodeBuilder.DefaultSort

  override x.CommandHandlerType with get() = typeof<FSharpProjectNodeCommandHandler>
