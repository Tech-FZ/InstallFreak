# InstallFreak Changelog

## Version 0.3.0.13_dev

### Changes

- The buttons have been moved down on the third page.
- Checkboxes for creating shortcuts have been made.
    - Their variables are passed to the install page.

### Known issues

- The shortcut checkboxes don't work yet.

## Version 0.2.0.12_dev

### Changes

- The redundant PowerShell scripts have been removed.
- The "Browse" button has a function prepared, but it ~~doesn't work yet~~ still needs testing.
    - Update 16th June, 2024 at 09:54 AM CEST: I had to fix the functions.
- There are efforts to make the first page more professional.

### Known issues

- ~~The "Browse" button doesn't work yet.~~ Resolved by Tech-FZ

## Version 0.1.1.11_dev

### Changes

- EmuGUI 2.0 Stable is there.
- I'm trying to replace the existing failed/succeeded pages with PowerShell scripts as fixing former doesn't work.
- The PowerShell scripts had to be fixed at first.
- Even though I tried to implement PowerShell scripts, it didn't even open as of yet. (15th June, 2024 at 10:51 AM CEST)
- An alternative approach using the install page for failed/succeeded messages as well is also being tested.

### Known issues

- InstallFreak is hard to get users pleased properly.

## Version 0.1.0.10_dev

### Changes

- As an effort to fix the InstallFreak issues, the randomizer for the temp folder has been removed.
- A background worker is also there to potentially fix the issue.
- To spice things up, I changed the temp folder to the application data folder used by C#.
- I also have to fix UI threading issues now.

## Version 0.0.4.9_proto

### Changes

- The third page now redirects to the install page when clicking "Next".
- A Finish page has been created.
- If the Installer ran into an error, it would redirect to a fail page.
- File cleaning (for temporary files and failed installation attempts) has been implemented.

### Known issues

- The program is yet to be tested.
- The program would download an older EmuGUI Pre-Release. Stable is not affected.

## Version 0.0.4.8_proto

- Instead of the depreciated WebClient, OctaneDownloader is used.
- The verification process is in the woks.
- Some minor corrections to the download location setter have been made.
- If everything works correctly, the installer itself would do its job now.

## Version 0.0.4.7_proto

- The page which actually installs the program is in the works.

## Version 0.0.3.6_proto

- The third page has been created as a summary page.
- Clicking "Next" on the second page now redirects to that third page.

## Version 0.0.2.5_proto

- The program welcomes you on the first page again.
- The layout on the first page has been adjusted a little.
- The Next button on the first page is now functional.
- The Previous button on the second page is also functional.
- The Cancel buttons also do their jobs now.

## Version 0.0.2.4_proto

- The second page has been adjusted and the table on there is made more useful.
- I tried to further improve the layout of the second page.
- You would also be able to set the installation path on the second page.
- The row and column definitions on the grid of the second page have been adjusted.
- The data grid has been adjusted.
- The installation path setting also comes with a Browse button.

## Version 0.0.2.3_proto

- The View Model has been altered for InstallFreak.
- A second page is in preparation.
- The first page has been adjusted and instead of "OK", that button says "Next".
- The Main Window opens to the second page by default (for now).
- I had to get rid of some unnecessary files and change the entire backend code so that the second page works at all.

## Version 0.0.1.2_proto

- Changes to the layout of the first page have been made.
- The first page has been made a separate file. On the main window, there is only a reference of it now.
- To make sure I can work with InstallFreak, I included the necessary information for the installation of EmuGUI.

## Version 0.0.1.1_proto

The first page and some documents are there.