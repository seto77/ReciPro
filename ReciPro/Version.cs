namespace ReciPro;

internal static class Version
{
    static public string Software =
       "ReciPro"
       ;

    static public string VersionAndDate => History.Remove(0, 10).Remove(20);

    static public string History =
        "History" +
        "\r\n ver4.836(2022/08/30)  The compiler for C++ code was changed to Clang." +
        "\r\n ver4.835(2022/08/09)  Improved compatibility for reading DM3 format files. " +
        "\r\n ver4.834(2022/07/08)  Improved the function to generate movies. " +
        "\r\n ver4.833(2022/06/24)  Added the function to generate movies for 'Structure Viewer'." +
        "\r\n ver4.832(2022/06/23)  Added the function to render stereonet projection with OpenGL." +
        "\r\n ver4.831(2022/05/14)  Fixed minor bugs on the HRTEM function." +
        "\r\n ver4.830(2022/04/14)  Some libraries are updated. Improved the HRTEM function (see https://github.com/seto77/ReciPro/issues/13)." +
        "\r\n ver4.829(2022/01/04)  Minor update on 'Spot ID v2' (see https://github.com/seto77/ReciPro/issues/11)." +
        "\r\n ver4.828(2021/12/15)  Updated the crystal database." +
        "\r\n ver4.827(2021/12/01)  Fixed a CultureInfo problem. (see https://github.com/seto77/ReciPro/issues/10)" +
        "\r\n ver4.826(2021/11/18)  Fixed minor bugs." +
        "\r\n ver4.820(2021/11/12)  Target framework has been changed to .Net 6.0." +
        "\r\n ver4.819(2021/10/27)  Improved an interface of Kikuchi line simulation. Speed up & fix bug on the dynamical diffraction simulator." +
        "\r\n ver4.817(2021/09/17)  Fixed minor bugs." +
        "\r\n ver4.815(2021/09/02)  Improved: User interfaces and tooltips." +
        "\r\n ver4.814(2021/08/29)  Fixed minor bugs: Drawing overlapping area of CBED disks (see https://github.com/seto77/ReciPro/issues/8)." +
        "\r\n ver4.813(2021/08/28)  Fixed minor bugs on HRTEM simulation (see https://github.com/seto77/ReciPro/issues/9)." +
        "\r\n ver4.812(2021/08/17)  Changed GUI. Fixed minor bugs." +
        "\r\n ver4.811(2021/08/10)  Fixed minor bugs on HRTEM simulation (see https://github.com/seto77/ReciPro/issues/7)." +
        "\r\n ver4.810(2021/08/07)  Fixed minor bugs." +
        "\r\n ver4.809(2021/07/16)  Fixed minor bugs. Renewed AMCSD database, and improved loading speed of the database." +
        "\r\n ver4.808(2021/07/08)  Fixed a minor bug about a compile option for native (c++) codes." +
        "\r\n ver4.807(2021/07/06)  Fixed minor bugs. Improved a rendering speed of 'Structure Viewer'." +
        "\r\n ver4.806(2021/05/25)  Fixed a distribution failure of language resource files." +
        "\r\n ver4.802(2021/05/24)  Target framework has been changed to .Net 5.0." +
        "\r\n ver4.800(2021/05/20)  Fixed bugs on native (c++) codes. Changed CBED interface." +
        "\r\n ver4.799(2021/05/10)  Fixed bugs on native (c++) codes." +
        "\r\n ver4.798(2021/05/03)  Fixed bugs on the 'Diffraction simulator'." +
        "\r\n ver4.797(2021/03/24)  Fixed a bug on the 'Database' function." +
        "\r\n ver4.795(2021/03/09)  Fixed a bug on the CBED calculation code." +
        "\r\n ver4.794(2021/03/08)  Added new algorithm for CBED calculation (matrix exponential method) " +
        "\r\n ver4.793(2021/02/26)  Fixed bugs in 'Diffraction simulator'." +
        "\r\n ver4.792(2020/12/28)  Fixed a bug on 'Parallels Desktop' for Mac (OpenGL drawing problem)." +
        "\r\n ver4.791(2020/11/06)  Fixed a bug when Kikuchi line drawing. Improved speed of 'Structure Viewer' drawing." +
        "\r\n ver4.790(2020/11/02)  Improved: GUI of 'Diffraction Simulator'." +
        "\r\n ver4.789(2020/10/26)  Improved: Speed up drawing of 'Diffraction Simulator'." +
        "\r\n ver4.788(2020/10/20)  Fixed a bug when calculating electron diffraction for crystals in AMCSD." +
        "\r\n ver4.787(2020/10/19)  Fixed bugs in 'Powder Diffraction'." +
        "\r\n ver4.786(2020/10/10)  Fixed bugs in 'Crystal Database' and improved the ’Find spots' function in 'Spot ID'." +
        "\r\n ver4.785(2020/10/06)  Fixed a problem on OpenGL with Radeon Vega graphics." +
        "\r\n ver4.784(2020/10/01)  Updated the manuals (both English and Japanese)." +
        "\r\n ver4.783(2020/09/08)  Fixed a bug on GUI." +
        "\r\n ver4.782(2020/08/19)  Fixed a bug on OpenGL." +
        "\r\n ver4.781(2020/08/19)  Loosen the restrictions on OpenGL requirements. (OpenGL 1.3 or higher)" +
        "\r\n ver4.780(2020/08/18)  Fixed a bug when exporting face-centered symmetry to CIF format. " +
        "\r\n ver4.779(2020/07/08)  Added a crystal database function, which manages 20698 crystals from AMCSD database. Fixed a bug on a dll file." +
        "\r\n ver4.778(2020/06/07)  Fixed a bug on importing CIF file." +
        "\r\n ver4.777(2020/06/06)  Improved GUI of the main window and 'structure viwer'." +
        "\r\n ver4.776(2020/05/30)  Improved: Speed up rendering of 'Structure viewer'." +
        "\r\n ver4.775(2020/05/19)  Improved: Rendering of text label in OpenGL windows. Fixed: Stereonet drawing." +
        "\r\n ver4.774(2020/05/15)  Fixed bugs for Wykcoff position discriminator for trigonal and hexagonal symmetries." +
        "\r\n ver4.773(2020/05/12)  Improved importing CIF file." +
        "\r\n ver4.772(2020/05/12)  Changed: Open GL 1.5 (or higher) is required for 'Structure Viewer'." +
        "\r\n ver4.771(2020/05/10)  Changed: Open GL 3.3 (or higher) is required for 'Structure Viewer'." +
        "\r\n ver4.770(2020/05/09)  Improved rendering quality of 'Structure Viewer'." +
        "\r\n ver4.769(2020/05/06)  Improved GUI on 'Structure Viewer'." +
        "\r\n ver4.768(2020/05/06)  Improved GUI on 'Structure Viewer'." +
        "\r\n ver4.767(2020/05/05)  Improved rendering speed of 'Structure Viewer' and fixed some bugs." +
        "\r\n ver4.766(2020/05/02)  Improved GUIs on 'Structure Viewer' and fixed bugs on 'Spot ID'." +
        "\r\n ver4.765(2020/04/26)  Improved 'Rotation geometry' and fixed 'Stereonet'." +
        "\r\n ver4.764(2020/04/12)  Improved GUI, and fixed minor bugs." +
        "\r\n ver4.763(2020/03/31)  Minor bugs fixed." +
        "\r\n ver4.762(2020/03/19)  Minor bugs fixed." +
        "\r\n ver4.761(2020/03/14)  Minor bugs fixed." +
        "\r\n ver4.760(2020/03/03)  Minor bugs fixed." +
        "\r\n ver4.756(2020/03/02)  Minor bugs fixed." +
        "\r\n ver4.755(2020/03/01)  Changed: Distribution site is changed to GitHub." +
        "\r\n ver4.747(2020/02/29)  Improved: Diffraction simulator." +
        "\r\n ver4.746(2020/02/28)  Improved: Diffraction simulator." +
        "\r\n ver4.745(2020/02/16)  Fixed a minor bug of Diffraction simulator." +
        "\r\n ver4.744(2020/02/05)  Improved interfaces of Diffraction simulator." +
        "\r\n ver4.743(2020/02/02)  Improved interfaces of Diffraction simulator." +
        "\r\n ver4.742(2020/01/06)  A minor improvement on SpotID. " +
        "\r\n ver4.741(2019/12/12)  Fixed a minor bug on SpotID. " +
        "\r\n ver4.740(2019/12/07)  Fixed a minor bug on TDS calculation. " +
        "\r\n ver4.739(2019/10/24)  Fixed a minor bug on 'Diffraction Simulator'. " +
        "\r\n ver4.733(2019/10/17)  Fixed a minor bug on 'Diffraction Simulator'. " +
        "\r\n ver4.731(2019/09/24)  Fixed minor bugs on HRTEM image simulation and Spot ID. " +
        "\r\n ver4.729(2019/09/16)  Improved interfaces of HRTEM image simulation. " +
        "\r\n ver4.728(2019/09/15)  Improved interfaces of HRTEM image simulation. " +
        "\r\n ver4.725(2019/09/11)  Improved calculation speed of HRTEM image simulation. " +
        "\r\n ver4.720(2019/09/09)  Improved calculation speed of HRTEM image simulation. " +
        "\r\n ver4.718(2019/09/08)  Fixed minor bugs on HRTEM image simulation. " +
        "\r\n ver4.715(2019/09/08)  Fixed minor bugs on HRTEM image simulation. " +
        "\r\n ver4.714(2019/09/07)  Improved calculation speed of HRTEM image simulation. " +
        "\r\n ver4.713(2019/09/06)  Fixed minor bugs on HRTEM image simulation. " +
        "\r\n ver4.711(2019/09/04)  Improved: HRTEM image simulation. Transmission cross coefficient model is added." +
        "\r\n ver4.704(2019/09/03)  Improved: HRTEM image simulation. Through-focus/defocus mode is now available." +
        "\r\n ver4.703(2019/08/26)  Add: HRTEM image simulation is now available. Very thanks to Dr. Ohtsuka." +
        "\r\n ver4.694(2019/08/18)  Fixed minor bugs on 'Diffraction Simulator'. Changed .Net framework version to 4.8" +
        "\r\n ver4.693(2019/08/06)  Fixed minor bugs on 'Spot ID'" +
        "\r\n ver4.692(2019/08/05)  Improved function on 'Spot ID'" +
        "\r\n ver4.687(2019/08/02)  Improved calculation speed for the PED simulation" +
        "\r\n ver4.686(2019/07/24)  Fixed minor bugs in PED simulation" +
        "\r\n ver4.683(2019/07/20)  Added a function: In 'Diffraction Simulator', precession electron diffraction (PED) mode is now available." +
        "\r\n ver4.682(2019/07/18)  Fixed a minor bug (eigen solver did not property work)." +
        "\r\n ver4.681(2019/07/08)  Fixed minor bugs on 'Spot ID'" +
        "\r\n ver4.680(2019/07/06)  Added 'Rotation Geometry' form." +
        "\r\n ver4.670(2019/06/12)  Improved functions on 'Spot ID'." +
        "\r\n ver4.669(2019/05/17)  Fixed minor bugs." +
        "\r\n ver4.668(2019/04/25)  Fixed minor bugs." +
        "\r\n ver4.667(2019/04/21)  Fixed minor bugs." +
        "\r\n ver4.664(2019/04/19)  Fixed minor bugs." +
        "\r\n ver4.663(2019/04/16)  Fixed minor bugs." +
        "\r\n ver4.662(2019/04/12)  Fixed minor bugs." +
        "\r\n ver4.661(2019/04/11)  Fixed minor bugs." +
        "\r\n ver4.660(2019/04/10)  Changed the installer. ClickOnce version will be not maintained in the future." +
        "\r\n ver4.654(2019/04/09)  Improved the update function for zip version." +
        "\r\n ver4.653(2019/04/08)  Fixed minor bugs." +
        "\r\n ver4.652(2019/04/04)  Fixed minor bugs." +
        "\r\n ver4.651(2019/03/27)  Corrected typos of Wyckoff positions and site symmetries in some space groups." +
        "\r\n ver4.650(2019/03/25)  Minor bugs fixed." +
        "\r\n ver4.649(2019/03/18)  Minor bugs fixed & Improved calculation speed of dynamic diffraction intensity." +
        "\r\n ver4.648(2019/03/11)  Fixed minor bugs and improved a calculation speed on 'SPot ID'" +
        "\r\n ver4.647(2019/03/10)  Fixed minor bugs on 'SPot ID'" +
        "\r\n ver4.646(2019/03/08)  Fixed minor bugs on 'SPot ID'" +
        "\r\n ver4.645(2019/03/07)  Fixed minor bugs on 'SPot ID'" +
        "\r\n ver4.643(2019/03/06)  Fixed minor bugs on 'SPot ID'" +
        "\r\n ver4.642(2019/03/05)  Improved calculation speed of 'SPot ID'" +
        "\r\n ver4.641(2019/03/04)  Improved calculation speed of 'SPot ID'" +
        "\r\n ver4.64 (2019/03/03)  Changed Visual Studio version to 2019." +
        "\r\n ver4.636(2019/03/01)  Improved some functions in 'SPot ID'." +
        "\r\n ver4.635(2019/02/28)  Improved some functions in 'SPot ID'." +
        "\r\n ver4.634(2019/02/27)  Improved some functions in 'SPot ID'." +
        "\r\n ver4.633(2019/02/26)  Improved some functions in 'SPot ID'." +
        "\r\n ver4.632(2019/02/22)  Improved some functions in 'SPot ID'." +
        "\r\n ver4.631(2019/02/21)  Fixed minor bugs. (copy functions in 'Structure Viewer' and 'Spot ID')" +
        "\r\n ver4.630(2019/02/20)  Fixed a minor bug. Changed .Net framework version to 4.7.2." +
        "\r\n ver4.629(2019/02/20)  Added a function: OpenGL can be manualy disabled by pressing 'CTRL' button on boot." +
        "\r\n ver4.628(2019/02/19)  Fixed a bug of calculations of anisotropic Debye-Waller efects." +
        "\r\n ver4.627(2019/02/17)  Minor bug fixed." +
        "\r\n ver4.625(2019/02/13)  Fixed minor bugs." +
        "\r\n ver4.624(2019/02/09)  Added: Check routine of OpenGL version." +
        "\r\n ver4.622(2019/02/06)  Minor improvements." +
        "\r\n ver4.621(2019/02/05)  Minor bug on the 'Spot ID' function fixed." +
        "\r\n ver4.620(2019/02/05)  Minor bug on the 'Spot ID' function fixed." +
        "\r\n ver4.619(2019/02/03)  Minor bug (in Bethe method) fixed." +
        "\r\n ver4.618(2019/01/28)  Minor bug fixed." +
        "\r\n ver4.617(2019/01/26)  Minor bug fixed." +
        "\r\n ver4.616(2019/01/25)  Minor bug fixed." +
        "\r\n ver4.615(2019/01/22)  Improved: A simulated CBED pattern can be saved as Tiff (32-bit float) format." +
        "\r\n ver4.614(2019/01/22)  Improved: Detailed results of the Bethe method calculation can be displayed." +
        "\r\n ver4.613(2019/01/19)  Minor improments on dynamic compression mode." +
        "\r\n ver4.612(2019/01/11)  Minor improments on dynamic compression mode." +
        "\r\n ver4.611(2019/01/08)  Fixed a minor bug on a TDS calculation." +
        "\r\n ver4.61 (2019/01/05)  Improved a dynamic simulation of electron diffraction. A TDS (thermal diffuse scattering) effect is now calculated properly" +
        "\r\n ver4.602(2018/12/23)  Improved 'Structure viewer'." +
        "\r\n ver4.601(2018/12/20)  Improved 'Structure viewer'." +
        "\r\n ver4.6  (2018/12/17)  Replaced OpenGL libraries. From this version, Open GL 4.3 (or higher) is required." +
        "\r\n ver4.515(2018/11/20)  Modified some incosistensies." +
        "\r\n ver4.514(2018/10/25)  Minor bug fixed." +
        "\r\n ver4.513(2018/10/22)  Improved calculation speed for CBED." +
        "\r\n ver4.512(2018/10/19)  Added a solver library for CBED calculation." +
        "\r\n ver4.511(2018/10/18)  Minor improvements to CBED calculation." +
        "\r\n ver4.51 (2018/10/16)  Minor improvements to CBED calculation." +
        "\r\n ver4.50 (2018/10/16)  Added a dynamic simulation mode (CBED pattern) by the Bethe method (beta). Very thanks to Dr. Ohtuka & Dr. Igami!" +
        "\r\n ver4.42 (2018/10/11)  Minor improvements." +
        "\r\n ver4.41 (2018/10/05)  Fixed minor bugs about the Bethe method." +
        "\r\n ver4.40 (2018/09/23)  Added a dynamic simulation mode (SAED pattern) by the Bethe method (beta)." +
        "\r\n ver4.372(2018/09/10)  Minor bug fixed." +
        "\r\n ver4.371(2018/08/27)  Fixed bugs on 'Single Crystal Diffraction' form. (thx Dr.Sakamoto)" +
        "\r\n ver4.362(2018/03/30)  Minor improvements." +
        "\r\n ver4.361(2018/03/23)  Minor improvements." +
        "\r\n ver4.36 (2018/03/19)  Improved: 'TEMID' is capable of selection of multiple crystals. (need Ctrl + Click)." +
        "\r\n ver4.35 (2018/03/01)  Improved an algorythm of 'Diffraction Simulator'." +
        "\r\n ver4.346(2018/02/23)  Minor bug fixed." +
        "\r\n ver4.345(2018/02/22)  Minor bug fixed." +
        "\r\n ver4.344(2018/02/22)  Minor bug fixed." +
        "\r\n ver4.343(2018/02/22)  Minor bug fixed." +
        "\r\n ver4.342(2018/02/21)  Added some options on 'Diffraction Simulator' which enable to copy detector area." +
        "\r\n ver4.341(2018/02/21)  Fixed a minor bug." +
        "\r\n ver4.34 (2018/02/20)  Improved. 'Diffraction Simulator' is now capable of selection of multiple crystals. (need Ctrl + Click)" +
        "\r\n ver4.334(2018/02/19)  Fixed minor bugs." +
        "\r\n ver4.333(2018/02/19)  Fixed minor bugs." +
        "\r\n ver4.332(2018/02/13)  Fixed minor bugs." +
        "\r\n ver4.331(2018/02/08)  Fixed minor bugs." +
        "\r\n ver4.33 (2018/02/07)  Improved: Rotation state is individually reserved in each crystal. " +
        "\r\n ver4.32 (2018/02/05)  Added: The nearest zone axis can be shown in the main form. " +
        "\r\n ver4.317(2018/02/03)  Fixed minor bugs on 'Single crystal diffraction' form. " +
        "\r\n ver4.316(2018/01/26)  Fixed minor bugs on 'Single crystal diffraction' form. " +
        "\r\n ver4.312(2018/01/25)  Improved 'Single crystal diffraction' form. " +
        "\r\n ver4.311(2018/01/20)  Improved the 'Operlap picture' function on 'Single crystal diffraction' form." +
        "\r\n ver4.31 (2018/01/19)  Changed graphics interfcace for 'Single crystal diffraction' form from OpenGL to GDI+, and then the metafile (vector object) of diffraction patterns can be exported to your clipboard. The 'Operlap picture' function is now under construction" +
        "\r\n ver4.30 (2017/12/24)  Changed graphics interfcace for 'Stereonet' form from OpenGL to GDI+, and then the metafile (vector object) of stereonet can be exported to your clipboard." +
        "\r\n ver4.29 (2017/09/01)  Added 'Point Spread' mode on 'Single Crystal Diffraction'." +
        "\r\n ver4.283(2017/05/28)  Fixed a smoll bug on 'Strain control' function'." +
        "\r\n ver4.282(2017/05/26)  Added 'Strain control' function'." +
        "\r\n ver4.281(2017/04/26)  Improved SACLA simulation on 'Single Crystal Diffraction'." +
        "\r\n ver4.280(2016/12/31)  Improved a compatibility for CIF format." +
        "\r\n ver4.279(2016/12/18)  Fixed minor bugs." +
        "\r\n ver4.278(2016/05/17)  Improved 'Powder Diffraction' and fixed minor bugs." +
        "\r\n ver4.277(2016/01/14)  Changed .Net Framework version to 4.6." +
        "\r\n ver4.276(2015/12/24)  Fixed a minor bug on initial loading." +
        "\r\n ver4.275(2015/12/23)  Fixed a minor bug on initial loading." +
        "\r\n ver4.273(2015/12/22)  Fixed a minor bug on initial loading." +
        "\r\n ver4.272(2015/12/18)  Fixed a minor bug on input form for rhombohedral settings." +
        "\r\n ver4.271(2015/12/11)  Fixed a minor bug on Wyckoff positions" +
        "\r\n ver4.270(2015/09/25)  Fixed a minor bug on 'Structure Viewer'.(thx Dr. Fukui)" +
        "\r\n ver4.269(2015/06/30)  Added: enable to simulate Back Laue camera." +
        "\r\n ver4.268(2015/05/13)  Fixed a minor bug on reading *.ipa files." +
        "\r\n ver4.267(2015/03/25)  Fixed a minor bug on sigle diffraction simulation" +
        "\r\n ver4.266(2015/03/18)  Fixed a bug on Debye-Waller factor calulations (thx Dr. Koga)" +
        "\r\n ver4.265(2015/03/13)  Fixed a bug about the calculation of the Wyckoff position of P63/mmc. (thx Dr. Nagasako)" +
        "\r\n ver4.264(2015/03/07)  Improved 'Spot ID' function." +
        "\r\n ver4.263(2015/01/28)  Improved 'Spot ID' function." +
        "\r\n ver4.262(2015/01/26)  Updated help files." +
        "\r\n ver4.261(2015/01/24)  Inmproved: a support of DM4 file on 'Spot ID'." +
        "\r\n ver4.26 (2015/01/23)  Added a new function, 'Spot ID', where diffraction spots could be semi-automatically identified." +
        "\r\n ver4.252(2014/11/11)  Fixed: minor bugs." +
        "\r\n ver4.251(2014/11/10)  Fixed: minor bugs." +
        "\r\n ver4.25 (2014/11/06)  Added: SACLA EH5 optics for single crystal diffraction mode." +
        "\r\n ver4.242(2014/10/27)  Fixed a bug on scattering factor information." +
        "\r\n ver4.241(2014/10/21)  Fixed minor bugs on OpenGL calculations." +
        "\r\n ver4.24 (2014/07/14)  Improved 'Powder Diffraction'. (but all function are not implemented yet)" +
        "\r\n ver4.234(2013/12/17)  Improved language option" +
        "\r\n ver4.233(2013/10/28)  Improved appearace for >100% DPI scaling" +
        "\r\n ver4.232(2013/10/15)  Improved appearace for >100% DPI scaling" +
        "\r\n ver4.231(2013/08/10)  Fixed minor bugs on OpenGL." +
        "\r\n ver4.23 (2013/03/28)  Improved structure viewer." +
        "\r\n ver4.221(2013/02/26)  Changed adress of help page." +
        "\r\n ver4.22 (2013/02/25)  Added: Update check function" +
        "\r\n ver4.21 (2013/02/21)  Added: CIF file export function" +
        "\r\n ver4.202(2012/12/20)  Fixed a small bug." +
        "\r\n ver4.201(2012/12/19)  Fixed a small bug." +
        "\r\n ver4.20 (2012/12/17)  Fixed OpenGL liblary." +
        "\r\n ver4.191(2012/12/05)  Fixed minor bugs." +
        "\r\n ver4.19 (2012/08/11)  Improved: appearance in TEMID window." +
        "\r\n ver4.184(2012/06/22)  Added: 'Reset registry keys' function was added in the 'Option' menu" +
        "\r\n ver4.183(2012/06/03)  Bug Fix" +
        "\r\n ver4.182(2012/06/01)  Bug Fix" +
        "\r\n ver4.181(2012/05/31)  Bug Fix" +
        "\r\n ver4.18 (2012/05/23)  Improved: speed up of calculation of Debye ring simulation." +
        "\r\n ver4.17 (2011/12/28)  Improved: Space groups A1, B1, C1, and F1 were added." +
        "\r\n ver4.161(2011/12/05)  Fixed: a small bug on debye ring simulation was fixed." +
        "\r\n ver4.16 (2011/12/04)  Improved: speed up of calculation of Debye ring simulation." +
        "\r\n ver4.15 (2011/11/24)  Improved: More strict polycrystalline diffraction pattern can be calculated considering beam convergence and monochromaticity." +
        "\r\n ver4.142(2011/11/20)  Improved: Stereonet simulator can draw vectors of specified indices selected by users." +
        "\r\n ver4.141(2011/11/07)  Fixed: PolycrystallineDiffractionSimulation" +
        "\r\n ver4.14 (2011/11/01)  Fixed the critical mistake on polycrystalline diffraction simulation: Intenisty calculation was corrected." +
        "\r\n ver4.131(2011/11/01)  Fixed: Y axis direction on polycrystalline diffraction simulation form was corrected." +
        "\r\n ver4.13 (2011/10/31)  Improved: Polycrystalline diffraction simulation; Fixed: File->Close function." +
        "\r\n ver4.12 (2011/10/31)  Improved: Polycrystalline diffraction simulation" +
        "\r\n ver4.113(2011/10/30)  Fixed a bug: projection buttons on main form in Japanese mode" +
        "\r\n ver4.112(2011/10/21)  Fixed a bug when sending crystal data." +
        "\r\n ver4.111(2011/10/17)  Fixed problems on Single Crystal Diffraction form." +
        "\r\n ver4.11 (2011/10/12)  Fixed problems on import CIF format." +
        "\r\n ver4.10 (2011/10/12)  Added language option. Englisgh and Japanese are available." +
        "\r\n ver4.00 (2011/07/19)  同位体組成の入出力と中性子線回折の強度計算に対応しました。" +
        "\r\n ver3.922(2011/07/05)  CrystalInformationがはみ出していたバグを修正" +
        "\r\n ver3.921(2011/07/05)  昨日の変更を微修正。空間群情報(Symmetry info.)と構造因子(Scattering factor)を分けて表示するようにしました。" +
        "\r\n ver3.92 (2011/07/04)  メインツールバーに「Detailed Information」を付けました。空間群の情報や、構造因子を表示できます。" +
        "\r\n ver3.91 (2011/05/10)  TEMIDで、等価な軸の判定ミスがありました。修正。" +
        "\r\n ver3.90 (2011/04/21)  DiffractionSimulator周りを改良。なかなか完成とまではいきませんが、とりあえず。" +
        "\r\n ver3.811(2011/02/29)  DiffractionSimulator周りを改良(中)。まだ途中ですが、要望があったので、とりあえず公開" +
        "\r\n ver3.81 (2010/11/18)  ヘルプページのリンク先を変更。内容は鋭意作成中です。" +
        "\r\n ver3.80 (2010/11/08)  初回起動時にバックグラウンドでネイティブコードを生成するように変更。二回目以降の起動が早くなります。" +
        "\r\n ver3.701(2010/11/08)  三斜晶系の対称性のコーディングミスを修正" +
        "\r\n ver3.70 (2010/11/07)  起動を高速化。多分数倍は速くなったと思います。" +
        "\r\n ver3.62 (2010/07/21)  StereoNet投影でSchmidtネット(等積投影)に対応しました。" +
        "\r\n ver3.61 (2010/05/09)  開発環境をVS2010にしました。" +
        "\r\n ver3.60 (2010/01/07)  結晶がランダムに配向したときのデバイリングパターンを表示できるようにしました。" +
        "\r\n ver3.59 (2009/12/24)  原子位置の計算に一部ミスがありました（特に複合格子の対称性）ので修正" +
        "\r\n ver3.58 (2009/10/20)  アプリ間の結晶データ送信が正常に行えなかったバグを修正" +
        "\r\n ver3.57 (2009/09/26)  Diffraction Simulatorでプリセッションカメラ(ZOLZ)を表示できるようにしました。&& X線の強度計算に対応　" +
        "\r\n ver3.56 (2009/09/24)  Electron Diffractionで画像をオーバーラップして表示できるようにしました。" +
        "\r\n ver3.55 (2009/09/03)  64bit OSに対応しました。" +
        "\r\n ver3.54 (2009/06/01)  ステレオネット描画の部分で色を変更できないバグを修正" +
        "\r\n ver3.53 (2009/03/11)  回折スポットの励起誤差、結晶構造因子を表示できるようにしました。表示が込み合ってしまうので、選択表示できるように考え中です。" +
        "\r\n ver3.52 (2009/03/10)  CIFファイルの読み込みバグを修正" +
        "\r\n ver3.51 (2008/10/30)  'Apply to same elements'の機能にバグがあったので修正しました。" +
        "\r\n ver3.50 (2008/08/31)  画像保存にバグがありましたので修正しました。" +
        "\r\n ver3.49 (2008/08/27)  StructureViewerで、凡例や結晶軸の画像も保存できるようにしました。" +
        "\r\n ver3.48 (2008/08/27)  Irregularな空間群A-1,B-1,C-1,I-1,F-1に対応しました。& StructureViewerの画像が保存できなかったのを修正。" +
        "\r\n ver3.47 (2008/08/26)  StructureViewerで背景色、文字色を変更できるようにしました。& 前回終了時の色を読み込むようにしました。" +
        "\r\n ver3.46 (2008/08/20)  CIFファイルの読み込み不具合を修正しました。" +
        "\r\n ver3.45 (2008/07/10)  大円描画機能を追加（というか復活)" +
        "\r\n ver3.44 (2008/06/20)  作者異動に伴いメールアドレスなど変更" +
        "\r\n ver3.43 (2008/04/29)  初期結晶ファイル中のSiO2 (CaCl2構造)の格子定数が間違っていたのを修正しました。" +
        "\r\n ver3.42 (2008/04/22)  読み込む/書き込む結晶を選択することができるようにしました。" +
        "\r\n ver3.41 (2008/04/13)  Smapのoutデータが読めなくなっていたバグを修正" +
        "\r\n ver3.40 (2008/02/28)  Structure Viewerで原子の配位状況を表示するようにしました。" +
        "\r\n ver3.39 (2008/02/24)  電子線回折強度の計算速度を若干高速化" +
        "\r\n ver3.38 (2008/02/23)  電子線回折強度の運動学的理論計算に対応しました。計算速度はこれから向上させていきます。" +
        "\r\n ver3.37 (2008/02/12)  結晶ファイルのドラッグドロップ対応&外部連携強化&デザイン変更" +
        "\r\n ver3.36 (2008/02/07)  バグ修正&デザイン変更" +
        "\r\n ver3.35 (2008/01/29)  ワイコフ位置の計算部分のバグ修正(いつまで見つかるやら…)。" +
        "\r\n ver3.34 (2008/01/28)  ワイコフ位置の計算部分のバグ修正。" +
        "\r\n ver3.33 (2008/01/28)  FormElectron(電子回折)の初期表示時に解像度設定がおかしくなってしまうのを修正 (永田さん、ありがとうございます) " +
        "\r\n ver3.32 (2008/01/25)  起動時にヒントをだせるようにしました。" +
        "\r\n ver3.31 (2008/01/21)  配布元を変更しました" +
        "\r\n ver3.30 (2008/01/21)  TEMIDの結果をダブルクリックすると回転角に反映する機能を追加" +
        "\r\n ver3.29 (2008/01/18)  ボタンイメージを入れてみました。" +
        "\r\n ver3.28 (2008/01/16)  一部デザインがおかしかったのを変更" +
        "\r\n ver3.27 (2008/01/14)  デザインを変更" +
        "\r\n ver3.26 (2008/01/10)  原子が一個だった時、凡例がうまく表示できなかったバグを修正" +
        "\r\n ver3.26 (2008/01/08)  フォームの誤動作を修正" +
        "\r\n ver3.25 (2008/01/07)  内部形式を変更 + 格子定数、原子位置の誤差に対応" +
        "\r\n ver3.24 (2007/12/26)  StructureViewerで原子選択後右クリックで、原子の配位環境を表示できるようにした (ほかのところにも拡張予定)" +
        "\r\n ver3.23 (2007/12/26)  ElectronDiffractionで面間隔、逆格子原点からの距離を表示できるようにした" +
        "\r\n ver3.22 (2007/12/21)  StructureViewerで原子の凡例が表示できるようにしました。" +
        "\r\n ver3.21 (2007/11/12)  StructureViewerでBondの一部が表示されないことがあったバグを修正" +
        "\r\n ver3.20 (2007/11/12)  StructureViewerにアニメーション(自動回転)機能追加。" +
        "\r\n ver3.19 (2007/11/09)  メインウィンドウに結晶軸の方向を表示するようにしました。" +
        "\r\n ver3.18 (2007/11/07)  印刷機能を付けました。" +
        "\r\n ver3.17 (2007/11/07)  StructureViewerで単位格子の稜が一本表示されていなかったバグを修正。ToolTipを充実。" +
        "\r\n ver3.16 (2007/11/03)  Stereonet, ElectronDiffractionで画像を保存、コピー機能追加 & 。ElectronDiffractionで色を変更できないバグを修正" +
        "\r\n ver3.15 (2007/10/27)  共通フォーム&コントロールの部分を分離しDLL化した" +
        "\r\n ver3.14 (2007/10/26)  カメラ長が変更できなかったバグを修正" +
        "\r\n ver3.13 (2007/09/26)  SMAP(http://www.sci.hokudai.ac.jp/~hiro/)の構造解析データを直接読み込めるようにしました。" +
        "\r\n ver3.12 (2007/08/21)  Structure ViewerのLattice Plane表示がおかしいバグを修正 && 全体的に速度向上" +
        "\r\n ver3.11 (2007/08/21)  突如落ちるなどのバグをさらにさらに改善。今度こそ？" +
        "\r\n ver3.10 (2007/08/15)  下記のバグをさらに改善。文字描画が難しい・・・" +
        "\r\n ver3.09 (2007/08/14)  StereoNet, ElectronDiffractionがたまに止まってしまうバグを改善" +
        "\r\n ver3.08 (2007/08/08)  OpenGL関係でバグ修正" +
        "\r\n ver3.07 (2007/08/07)  StereoNet,ElectronDiffractionをOpenGL描画に変更しました。速くなりましたがまだバグがあるかも・・・" +
        "\r\n ver3.06 (2007/07/05)  結晶軸計算のバグを修正" +
        "\r\n ver3.05 (2007/07/05)  TEMID部分の機能を追加(対称性チェック、複数パターンからの抽出など)" +
        "\r\n ver3.04 (2007/06/22)  ホームページアドレスを変更" +
        "\r\n ver3.03 (2007/06/22)  選択している対称性に関する情報を表示できるようにしました。" +
        "\r\n ver3.02 (2007/06/21)  結晶データをPDIndexerと同一形式のxmlファイルでリスト化して読み込めるようにしました。" +
        "\r\n ver3.01 (2007/06/10)  Electron diffraction, TEMIDフォームを追加" +
        "\r\n ver3.00 (2007/05/30)  ベータ版作成。ver2.40から大幅に作り直す。未だ道の途中" +
        "\r\n ver2.40 (2004/05/13)  StereoNetに大円描画機能追加&&StereoNet,Geometricsにワイコフ位置情報を追加" +
        "\r\n ver2.31 (2003/11/12)  バグフィックス&&StereoNet, Diffractionの改良" +
        "\r\n ver2.30 (2003/11/12)  データベース機能を追加&&設定ファイルの形式変更&&StereoNet, Diffraction の画像出力機能追加" +
        "\r\n ver2.20 (2003/10/28)  菊池線表示機能追加&&一部デザイン変更" +
        "\r\n ver2.12 (2003/10/23)  バグフィックス" +
        "\r\n ver2.11 (2003/10/13)  一部のデザインを変更&&描画部の高速化とバグフィックス" +
        "\r\n ver2.10 (2003/10/04)  TemIDのデザイン(パターン入力部)を改良&&Helpファイルを充実" +
        "\r\n ver2.00 (2003/09/28)  開発環境を「.Net Framework」に変更&&画像解析機能追加&&" +
                                    "TemID解析結果をステレオネット、逆格子空間に反映する機能を追加" +
        "\r\n ver1.05 (2002/05/01)  Tilt,Azimuth,Rotationの説明ダイアログ追加&&Ewald球の半径の調節スライドバーを追加&&" +
                                    "逆格子点表示をスピードアップ&&バグフィックス" +
        "\r\n ver1.04 (2002/04/22)  逆空間を表示する機能を追加&&バグフィックス" +
        "\r\n ver1.03 (2002/03/30)  ステレオネットの拡大縮小機能を追加&&空間群の間違いを訂正&&バグフィックス" +
        "\r\n ver1.02 (2002/03/28)  ステレオネットの機能を追加&&バグフィックス" +
        "\r\n ver1.01 (2002/03/14)  空間群の間違いを訂正&PHOTO間のリンクボタン追加&3点の回折斑点から解析するモード追加" +
        "\r\n ver1.00 (2002/03/03)  暫定動作バージョンを作成"
        ;



    /// <summary>
    /// はじめに
    /// </summary>
    static public string Introduction =
        "Introduction:"
        + "\r\nThe software “ReciPro” supports many crystallographic calculations, simulation of/indexing  diffraction pattern, and so on."
        + "\r\n - ReciPro provides many crystallographic calculations for 530 (Hall symbol) space groups."
        + "\r\n - ReciPro includes many atomic properties."
        + "\r\n - ReciPro can simulate diffraction patterns for various optics."
        + "\r\n - ReciPro draws crystal structure using OpenGL4"
        + "\r\n - ReciPro plots planes and axes to stereonet."
        + "\r\n - ReciPro identifies diffraction spots in the observed image."
        ;

    /// <summary>
    /// 謝辞
    /// </summary>
    static public string Acknowledge =
        "Acknowledgement:"
        + "\r\n　Thanks to Daisuke Hamane (Tokyo Univ.), Masahiro Ohtsuka (Nagoya Univ.),  Akira Miyake (Kyoto Univ.)"
        + "Shoichi Toh (Kyushu Univ.), and many other contributers"
        ;

    /// <summary>
    /// 使い方
    /// </summary>
    static public string Manual =
        "Manual:"
        + "\r\n　See \"Help\" => \"Help\""
        ;

    /// <summary>
    /// 著作権
    /// </summary>
    static public string CopyRight =
        "Copyrights:"
        + "\r\n　Dr. Y. Seto holds the copyrights of ReciPro."
        ;

    /// <summary>
    /// 使用条件
    /// </summary>
    static public string Condition =
        "Licene:"
        + "\r\n ReciPro is a　free (non-charge) software for academic, scientific, and educational users."
        + "\r\n The people or companies engaged in commercial enterprise may also use ReciPro for their buisiness."
        + "\r\n The use of ReciPro is limited to non-military and non-illegal purposes."
        ;


    /// <summary>
    /// 免責事項
    /// </summary>
    static public string Exemption =
        "Disclaimer:"
        + "\r\n ReciPro is provided by the author \"as is\" and \"with all faults.\" "
        + "\r\n The author makes no warranties of any kind concerning the safety, lack of viruses, inaccuracies, or other harmful components of ReciPro."
        + "\r\n The author will not be liable for any damages you may suffer in connection with using ReciPro."
        ;

    /// <summary>
    /// 連絡先
    /// </summary>
    static public string Adress =
        "Contact:"
        + "\r\n If you find any problems of ReciPro to be improved, feel free to contact me by followin e-mail;"
        + "\r\n mail: seto@crystal.kobe-u.ac.jp"
        + "\r\n Home Page: http://pmsl.planet.sci.kobe-u.ac.jp/~seto/"
        ;

    static public string[] Hint = new string[]{
         //   "キーボードショートカット\r\n   Ctrl + Shift + V : Structure Viewer\r\n   Ctrl + Shift + S : Sereonet\r\n"+
         //   "   Ctrl + Shift + D : Electron Diffraction\r\n   Ctrl + Shift + T : TEM ID\r\n   Ctrl + Shift + 矢印キー : 回転操作",
         //   "Ctrlをすばやく2回押すと簡易関数電卓が立ち上がります。",
         //   "'Crystal Information'部分にCIF形式およびAMC形式のファイルをドラッグドロップすると、結晶構造を読み込むことができます。('Add'をするとリストに加えることができます。)",
         //   "Crystal List(xml形式)は拙作ソフトReciProと同形式です。互いに編集したファイルを読み書きすることができます。",
         //   "拙作ソフト'CSManager'で結晶を選択すると、クリップボード経由で'Crystal Information'部分に結晶が読み込まれます。('Add'をするとリストに加えることができます。)",
         //   "Structure Viewerでは結晶構造を描くほかに、単位格子や結晶面の描画を行うことができます。",
         //   "TEM IDでの検索された晶帯軸を含む行をダブルクリックすると、その晶帯軸が入射方向に設定されます。",
         //   "回転の具合がおかしいな？と思ったらメインウィンドウで'Reset Rotation'してください。",
         //   "'Stereonet'では画面をダブルクリックすると軸・面を変更できます",
         //   "'Electron Diffraction'で'Kinematical Simulation'をチェックすると運動学的回折理論による回折強度を計算します。回折強度は回折斑点の大きさに比例しています。"
        };




}
