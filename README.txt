Twitchbot für einen Coworking-Stream auf Twitch mit eigenen Chat-Commands. Chatteilnehmer können Aufgaben zu einer To-Do-Liste, welche im Stream angezeigt werden kann, hinzufügen, editieren, löschen und als "erledigt" markieren. Darüber hinaus wurden in dieser Version auf Wunsch des Streamers noch einige weitere Chat-Commands hinzugefügt.
Version: 2022

----- SETUP -----
- set channel (Bot.cs)
- set paths (FileManager.cs)

----- COMMANDS -----

!addtask <task>
- response: <user> added a task: <task>! akatri2Work 
- alt response: <user> you already added a task! Remove or finish your task first! akatri2Pew 
- adds username + task to txt-file


!removetask
- response: <user> canceled a task: <task>! akatri2Pew 
- removes user's task in txt-file


!taskdone
- response: CONGRATS <user>! akatri2Hype YOU COMPLETED YOUR TASK! <task> IS DONE! akatri2Party akatri2Lovings 
- alt response: <user> you have to add a task to finish a task! akatri2Pew 
- removes user's task in txt-file


!edittask <task>
- response: <user> edited the task: <task>! akatri2Work
- edits user's task in txt-file 

!finishedtasks
!allfinishedtasks

!hello
- response: hello bio! you are so loved! hihi <3
- alt response: you're not bio :c
