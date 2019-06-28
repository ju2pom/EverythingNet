using EverythingNet.Core;
using NUnit.Framework;

namespace EverythingNet.Tests
{
  using System.Linq;

  using EverythingNet.Query;

  [TestFixture]
  public class FileSearchTests
  {
    [TestCase("", ExpectedResult = "ext:aac;ac3;aif;aifc;aiff;au;cda;dts;fla;flac;it;m1a;m2a;m3u;m4a;mid;midi;mka;mod;mp2;mp3;mpa;ogg;ra;rmi;spc;rmi;snd;umx;voc;wav;wma;xm")]
    [TestCase(null, ExpectedResult = "ext:aac;ac3;aif;aifc;aiff;au;cda;dts;fla;flac;it;m1a;m2a;m3u;m4a;mid;midi;mka;mod;mp2;mp3;mpa;ogg;ra;rmi;spc;rmi;snd;umx;voc;wav;wma;xm")]
    [TestCase("music.mp3", ExpectedResult = "ext:aac;ac3;aif;aifc;aiff;au;cda;dts;fla;flac;it;m1a;m2a;m3u;m4a;mid;midi;mka;mod;mp2;mp3;mpa;ogg;ra;rmi;spc;rmi;snd;umx;voc;wav;wma;xm music.mp3")]
    public string Audio(string search)
    {
      var query = new Query().File.Audio(search);

      return query.ToString();
    }

    [TestCase("", ExpectedResult = "ext:7z;ace;arj;bz2;cab;gz;gzip;jar;r00;r01;r02;r03;r04;r05;r06;r07;r08;r09;r10;r11;r12;r13;r14;r15;r16;r17;r18;r19;r20;r21;r22;r23;r24;r25;r26;r27;r28;r29;rar;tar;tgz;z;zip")]
    [TestCase(null, ExpectedResult = "ext:7z;ace;arj;bz2;cab;gz;gzip;jar;r00;r01;r02;r03;r04;r05;r06;r07;r08;r09;r10;r11;r12;r13;r14;r15;r16;r17;r18;r19;r20;r21;r22;r23;r24;r25;r26;r27;r28;r29;rar;tar;tgz;z;zip")]
    [TestCase("archive.zip", ExpectedResult = "ext:7z;ace;arj;bz2;cab;gz;gzip;jar;r00;r01;r02;r03;r04;r05;r06;r07;r08;r09;r10;r11;r12;r13;r14;r15;r16;r17;r18;r19;r20;r21;r22;r23;r24;r25;r26;r27;r28;r29;rar;tar;tgz;z;zip archive.zip")]
    public string Zip(string search)
    {
      var query = new Query().File.Zip(search);

      return query.ToString();
    }


    [TestCase("", ExpectedResult = "ext:3g2;3gp;3gp2;3gpp;amr;amv;asf;avi;bdmv;bik;d2v;divx;drc;dsa;dsm;dss;dsv;evo;f4v;flc;fli;flic;flv;hdmov;ifo;ivf;m1v;m2p;m2t;m2ts;m2v;m4b;m4p;m4v;mkv;mp2v;mp4;mp4v;mpe;mpeg;mpg;mpls;mpv2;mpv4;mov;mts;ogm;ogv;pss;pva;qt;ram;ratdvd;rm;rmm;rmvb;roq;rpm;smil;smk;swf;tp;tpr;ts;vob;vp6;webm;wm;wmp;wmv")]
    [TestCase(null, ExpectedResult = "ext:3g2;3gp;3gp2;3gpp;amr;amv;asf;avi;bdmv;bik;d2v;divx;drc;dsa;dsm;dss;dsv;evo;f4v;flc;fli;flic;flv;hdmov;ifo;ivf;m1v;m2p;m2t;m2ts;m2v;m4b;m4p;m4v;mkv;mp2v;mp4;mp4v;mpe;mpeg;mpg;mpls;mpv2;mpv4;mov;mts;ogm;ogv;pss;pva;qt;ram;ratdvd;rm;rmm;rmvb;roq;rpm;smil;smk;swf;tp;tpr;ts;vob;vp6;webm;wm;wmp;wmv")]
    [TestCase("movie.avi", ExpectedResult = "ext:3g2;3gp;3gp2;3gpp;amr;amv;asf;avi;bdmv;bik;d2v;divx;drc;dsa;dsm;dss;dsv;evo;f4v;flc;fli;flic;flv;hdmov;ifo;ivf;m1v;m2p;m2t;m2ts;m2v;m4b;m4p;m4v;mkv;mp2v;mp4;mp4v;mpe;mpeg;mpg;mpls;mpv2;mpv4;mov;mts;ogm;ogv;pss;pva;qt;ram;ratdvd;rm;rmm;rmvb;roq;rpm;smil;smk;swf;tp;tpr;ts;vob;vp6;webm;wm;wmp;wmv movie.avi")]
    public string Video(string search)
    {
      var query = new Query().File.Video(search);

      return query.ToString();
    }

    [TestCase("", ExpectedResult = "ext:ani;bmp;gif;ico;jpe;jpeg;jpg;pcx;png;psd;tga;tif;tiff;wmf")]
    [TestCase(null, ExpectedResult = "ext:ani;bmp;gif;ico;jpe;jpeg;jpg;pcx;png;psd;tga;tif;tiff;wmf")]
    [TestCase("image.jpg", ExpectedResult = "ext:ani;bmp;gif;ico;jpe;jpeg;jpg;pcx;png;psd;tga;tif;tiff;wmf image.jpg")]
    public string Picture(string search)
    {
      var query = new Query().File.Picture(search);

      return query.ToString();
    }

    [TestCase("", ExpectedResult = "ext:bat;cmd;exe;msi;msp;msu;scr")]
    [TestCase(null, ExpectedResult = "ext:bat;cmd;exe;msi;msp;msu;scr")]
    [TestCase("application.exe", ExpectedResult = "ext:bat;cmd;exe;msi;msp;msu;scr application.exe")]
    public string Exe(string search)
    {
      var query = new Query().File.Exe(search);

      return query.ToString();
    }

    [TestCase("", ExpectedResult = "ext:c;chm;cpp;csv;cxx;doc;docm;docx;dot;dotm;dotx;h;hpp;htm;html;hxx;ini;java;lua;mht;mhtml;odt;pdf;potx;potm;ppam;ppsm;ppsx;pps;ppt;pptm;pptx;rtf;sldm;sldx;thmx;txt;vsd;wpd;wps;wri;xlam;xls;xlsb;xlsm;xlsx;xltm;xltx;xml")]
    [TestCase(null, ExpectedResult = "ext:c;chm;cpp;csv;cxx;doc;docm;docx;dot;dotm;dotx;h;hpp;htm;html;hxx;ini;java;lua;mht;mhtml;odt;pdf;potx;potm;ppam;ppsm;ppsx;pps;ppt;pptm;pptx;rtf;sldm;sldx;thmx;txt;vsd;wpd;wps;wri;xlam;xls;xlsb;xlsm;xlsx;xltm;xltx;xml")]
    [TestCase("report.doc", ExpectedResult = "ext:c;chm;cpp;csv;cxx;doc;docm;docx;dot;dotm;dotx;h;hpp;htm;html;hxx;ini;java;lua;mht;mhtml;odt;pdf;potx;potm;ppam;ppsm;ppsx;pps;ppt;pptm;pptx;rtf;sldm;sldx;thmx;txt;vsd;wpd;wps;wri;xlam;xls;xlsb;xlsm;xlsx;xltm;xltx;xml report.doc")]
    public string Document(string search)
    {
      var query = new Query().File.Document(search);

      return query.ToString();
    }

    [TestCase("", ExpectedResult = "dupe:")]
    [TestCase(null, ExpectedResult = "dupe:")]
    [TestCase("main.cs", ExpectedResult = "dupe:main.cs")]
    public string Duplicates(string search)
    {
      var query = new Query().File.Duplicates(search);

      return query.ToString();
    }

    [Test]
    public void Roots()
    {
      var query = new Query().File.Roots();

      Assert.That(query.ToString(), Is.EqualTo("root:"));
    }

    [Test]
    public void AcceptanceTest()
    {
      using (var everything = new Everything())
      {
        var query = new Query().File.Exe();

        Assert.That(everything.Search(query).Count, Is.GreaterThan(0));
      }
    }
  }
}
