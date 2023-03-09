# Phantom-Terminal





![PT sh](https://user-images.githubusercontent.com/112493528/223960503-fc83da11-4cb5-4977-a37e-a6edc1d3886c.png)




### Adding a student: `student -a`
<details><summary>Adds a student to the students list.</summary>

Format: `student -a -n NAME [-c CONTACT] [-em EMAIL] [-s DAY/STARTTIME/ENDTIME]​`

* Adds a student with the specified `NAME`
* `-a` refers to the add command
* Commands in `[ ]` are optional
* `DAY` takes in the following inputs: `MON TUE WED THU FRI SAT SUN` while `STARTTIME` and `ENDTIME` takes in the time in 24 hour format, for example 0800 for 8am.


Examples:
* `student -a -n John -c 12345678 -em john@mail.com -s TUE/1600/1800`<br>
  adds a student with the name John, contact number 12345678, email john@mail.com and a lesson every tuesday from 4pm to 6pm
* `student -a -n Barbara -c 12344321`<br>
  adds a student with the name Barbara and contact number 12344321

</details>





Shield: [![CC BY-NC-SA 4.0][cc-by-nc-sa-shield]][cc-by-nc-sa]

This work is licensed under a
[Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License][cc-by-nc-sa].

[![CC BY-NC-SA 4.0][cc-by-nc-sa-image]][cc-by-nc-sa]

[cc-by-nc-sa]: http://creativecommons.org/licenses/by-nc-sa/4.0/
[cc-by-nc-sa-image]: https://licensebuttons.net/l/by-nc-sa/4.0/88x31.png
[cc-by-nc-sa-shield]: https://img.shields.io/badge/License-CC%20BY--NC--SA%204.0-lightgrey.svg
