using System.IO;

namespace Crystallography;

public class CCP4
{
    private static readonly uint[] setbits =
                     {0x00000000, 0x00000001, 0x00000003, 0x00000007,
                          0x0000000F, 0x0000001F, 0x0000003F, 0x0000007F,
                          0x000000FF, 0x000001FF, 0x000003FF, 0x000007FF,
                          0x00000FFF, 0x00001FFF, 0x00003FFF, 0x00007FFF,
                          0x0000FFFF, 0x0001FFFF, 0x0003FFFF, 0x0007FFFF,
                          0x000FFFFF, 0x001FFFFF, 0x003FFFFF, 0x007FFFFF,
                          0x00FFFFFF, 0x01FFFFFF, 0x03FFFFFF, 0x07FFFFFF,
                          0x0FFFFFFF, 0x1FFFFFFF, 0x3FFFFFFF, 0x7FFFFFFF,
                          0xFFFFFFFF};

    private static uint shift_left(uint x, int n)
    { return (((x) & (uint)setbits[32 - (n)]) << (n)); }

    private static uint shift_right(uint x, int n)
    { return (((x) >> (n)) & (uint)setbits[32 - (n)]); }

    public static unsafe uint[] unpack(BinaryReader br, int x, int y)
    {
        int valids = 0, spillbits = 0, usedbits;
        int total = x * y;
        uint window = 0;
        uint spill = 0, pixel = 0, nextint;
        int bitnum;
        int pixnum;
        int[] bitdecode = new int[] { 0, 4, 5, 6, 7, 8, 16, 32 };
        uint[] img = new uint[total];

        /*  while (pixel < total)
          {
              if (valids < 6)
              {
                  if (spillbits > 0)
                  {
                      window |= shift_left(spill, valids);
                      valids += spillbits;
                      spillbits = 0;
                  }
                  else
                  {
                      spill = br.ReadByte();
                      spillbits = 8;
                  }
              }
              else
              {
                  pixnum = 1 << (int)(window & setbits[3]);
                  window = shift_right(window, 3);
                  bitnum = bitdecode[window & setbits[3]];
                  window = shift_right(window, 3);
                  valids -= 6;
                  while ((pixnum > 0) && (pixel < total))
                  {
                      if (valids < bitnum)
                      {
                          if (spillbits > 0)
                          {
                              window |= shift_left(spill, valids);
                              if ((32 - valids) > spillbits)
                              {
                                  valids += spillbits;
                                  spillbits = 0;
                              }
                              else
                              {
                                  usedbits = 32 - valids;
                                  spill = shift_right(spill, usedbits);
                                  spillbits -= usedbits;
                                  valids = 32;
                              }
                          }
                          else
                          {
                              spill = br.ReadByte();
                              spillbits = 8;
                          }
                      }
                      else
                      {
                          --pixnum;
                          if (bitnum == 0)
                              nextint = 0;
                          else
                          {
                              nextint = window & setbits[bitnum];
                              valids -= bitnum;
                              window = shift_right(window, bitnum);
                              if ((nextint & (1 << (bitnum - 1))) != 0)
                                  nextint |= ~setbits[bitnum];
                          }

                          if (pixel > x)
                              img[pixel] = (nextint + (img[pixel - 1] + img[pixel - x + 1] + img[pixel - x] + img[pixel - x - 1] + 2) / 4);
                          else if (pixel != 0)
                              img[pixel] = (img[pixel - 1] + nextint);
                          else
                              img[pixel] = nextint;
                          pixel++;
                      }
                  }
              }
          }*/

        while (pixel < total)
        {
            if (valids < 6)
            {
                if (spillbits > 0)
                {
                    window |= shift_left(spill, valids);
                    valids += spillbits;
                    spillbits = 0;
                }
                else
                {
                    spill = br.ReadByte();
                    spillbits = 8;
                }
            }
            else
            {
                pixnum = 1 << (int)(window & setbits[3]);
                window = shift_right(window, 3);
                bitnum = bitdecode[window & setbits[3]];
                window = shift_right(window, 3);
                valids -= 6;
                while ((pixnum > 0) && (pixel < total))
                {
                    if (valids < bitnum)
                    {
                        if (spillbits > 0)
                        {
                            window |= shift_left(spill, valids);
                            if ((32 - valids) > spillbits)
                            {
                                valids += spillbits;
                                spillbits = 0;
                            }
                            else
                            {
                                usedbits = 32 - valids;
                                spill = shift_right(spill, usedbits);
                                spillbits -= usedbits;
                                valids = 32;
                            }
                        }
                        else
                        {
                            spill = br.ReadByte();
                            spillbits = 8;
                        }
                    }
                    else
                    {
                        --pixnum;
                        if (bitnum == 0)
                            nextint = 0;
                        else
                        {
                            nextint = window & setbits[bitnum];
                            valids -= bitnum;
                            window = shift_right(window, bitnum);
                            if ((nextint & (1 << (bitnum - 1))) != 0)
                                nextint |= (~(setbits[bitnum]));
                        }
                        if (pixel > x)
                        {
                            img[pixel] = (nextint +
                                              (img[pixel - 1] + img[pixel - x + 1] +
                                               img[pixel - x] + img[pixel - x - 1] + 2) / 4);
                            ++pixel;
                        }
                        else if (pixel != 0)
                        {
                            img[pixel] = (img[pixel - 1] + nextint);
                            ++pixel;
                        }
                        else
                            img[pixel++] = nextint;
                    }
                }
            }
        }
        return img;
    }

    public static unsafe uint[] unpack_long(BinaryReader br, int x, int y)
    // void unpack_long(FILE *packfile, int x, int y, LONG *img)
    {
        int valids = 0, spillbits = 0, usedbits;
        int total = x * y;
        uint window = 0;
        uint spill = 0, pixel = 0, nextint;
        int bitnum;
        int pixnum;
        int[] bitdecode = new int[] { 0, 4, 5, 6, 7, 8, 16, 32 };
        uint[] img = new uint[total];

        //int valids = 0, spillbits = 0, usedbits, total = x * y;
        //long window = 0L, spill = 0, pixel = 0, nextint, bitnum, pixnum;
        //int bitdecode = new []{0, 4, 5, 6, 7, 8, 16, 32};

        while (pixel < total)
        {
            if (valids < 6)
            {
                if (spillbits > 0)
                {
                    window |= shift_left(spill, valids);
                    valids += spillbits;
                    spillbits = 0;
                }
                else
                {
                    spill = br.ReadByte();
                    spillbits = 8;
                }
            }
            else
            {
                pixnum = 1 << (int)(window & setbits[3]);
                window = shift_right(window, 3);
                bitnum = bitdecode[window & setbits[3]];
                window = shift_right(window, 3);
                valids -= 6;
                while ((pixnum > 0) && (pixel < total))
                {
                    if (valids < bitnum)
                    {
                        if (spillbits > 0)
                        {
                            window |= shift_left(spill, valids);
                            if ((32 - valids) > spillbits)
                            {
                                valids += spillbits;
                                spillbits = 0;
                            }
                            else
                            {
                                usedbits = 32 - valids;
                                spill = shift_right(spill, usedbits);
                                spillbits -= usedbits;
                                valids = 32;
                            }
                        }
                        else
                        {
                            spill = br.ReadByte();
                            spillbits = 8;
                        }
                    }
                    else
                    {
                        --pixnum;
                        if (bitnum == 0)
                            nextint = 0;
                        else
                        {
                            nextint = window & setbits[bitnum];
                            valids -= bitnum;
                            window = shift_right(window, bitnum);
                            if ((nextint & (1 << (bitnum - 1))) != 0)
                                nextint |= ~setbits[bitnum];
                        }
                        if (pixel > x)
                        {
                            img[pixel] = (nextint +
                                            (img[pixel - 1] + img[pixel - x + 1] +
                                           img[pixel - x] + img[pixel - x - 1] + 2) / 4);
                            ++pixel;
                        }
                        else if (pixel != 0)
                        {
                            img[pixel] = (img[pixel - 1] + nextint);
                            ++pixel;
                        }
                        else
                            img[pixel++] = nextint;
                    }
                }
            }
        }
        return img;
    }

    public static unsafe uint[] v2unpack(BinaryReader br, int x, int y)
    {
        int valids = 0, spillbits = 0, usedbits;
        int total = x * y;
        uint window = 0;
        uint spill = 0, pixel = 0, nextint;
        int bitnum;
        int pixnum;
        int[] bitdecode = new int[] { 0, 4, 5, 6, 7, 8, 16, 32 };
        uint[] img = new uint[total];

        while (pixel < total)
        {
            if (valids < 7)
            {
                if (spillbits > 0)
                {
                    window |= shift_left(spill, valids);
                    valids += spillbits;
                    spillbits = 0;
                }
                else
                {
                    spill = br.ReadByte();
                    spillbits = 8;
                }
            }
            else
            {
                pixnum = 1 << (int)(window & (int)setbits[3]);
                window = shift_right(window, 3);
                bitnum = bitdecode[window & setbits[4]];
                window = shift_right(window, 4);
                valids -= 7;
                while ((pixnum > 0) && (pixel < total))
                {
                    if (valids < bitnum)
                    {
                        if (spillbits > 0)
                        {
                            window |= shift_left(spill, valids);
                            if ((32 - valids) > spillbits)
                            {
                                valids += spillbits;
                                spillbits = 0;
                            }
                            else
                            {
                                usedbits = 32 - valids;
                                spill = shift_right(spill, usedbits);
                                spillbits -= usedbits;
                                valids = 32;
                            }
                        }
                        else
                        {
                            spill = br.ReadByte();
                            spillbits = 8;
                        }
                    }
                    else
                    {
                        --pixnum;
                        if (bitnum == 0)
                            nextint = 0;
                        else
                        {
                            nextint = window & setbits[bitnum];
                            valids -= bitnum;
                            window = shift_right(window, bitnum);
                            if ((nextint & (1 << (bitnum - 1))) != 0)
                                nextint |= ~setbits[bitnum];
                        }
                        if (pixel > x)
                        {
                            img[pixel] = (uint)(nextint +
                                          (img[pixel - 1] + img[pixel - x + 1] +
                                                           img[pixel - x] + img[pixel - x - 1] + 2) / 4);
                            ++pixel;
                        }
                        else if (pixel != 0)
                        {
                            img[pixel] = (uint)(img[pixel - 1] + nextint);
                            ++pixel;
                        }
                        else
                            img[pixel++] = (uint)nextint;
                    }
                }
            }
        }
        return img;
    }

    /*
             * /***********************************************************************
     *
     * marControl: pck.c
     *
     * Copyright by:        Dr. Claudio Klein
     *                      X-ray Research GmbH, Hamburg
     *
     * Version:     1.1
     * Date:        30/10/1995
     *
     ***********************************************************************

    #include <stdio.h>
    #include <stddef.h>
    #include <math.h>
    #include <ctype.h>
    #include <string.h>

    #ifdef _MSC_VER
    #include <io.h>
    #include <malloc.h>
    #endif

    #define BYTE char
    #define WORD short int
    #define LONG long int

    #define PACKIDENTIFIER "\nCCP4 packed image, X: %04d, Y: %04d\n"
    #define PACKBUFSIZ BUFSIZ
    #define DIFFBUFSIZ 16384L
    #define max(x, y) (((x) > (y)) ? (x) : (y))
    #define min(x, y) (((x) < (y)) ? (x) : (y))
    #define abs(x) (((x) < 0) ? (-(x)) : (x))
    const LONG setbits[33] = {0x00000000L, 0x00000001L, 0x00000003L, 0x00000007L,
                  0x0000000FL, 0x0000001FL, 0x0000003FL, 0x0000007FL,
                  0x000000FFL, 0x000001FFL, 0x000003FFL, 0x000007FFL,
                  0x00000FFFL, 0x00001FFFL, 0x00003FFFL, 0x00007FFFL,
                  0x0000FFFFL, 0x0001FFFFL, 0x0003FFFFL, 0x0007FFFFL,
                  0x000FFFFFL, 0x001FFFFFL, 0x003FFFFFL, 0x007FFFFFL,
                  0x00FFFFFFL, 0x01FFFFFFL, 0x03FFFFFFL, 0x07FFFFFFL,
                  0x0FFFFFFFL, 0x1FFFFFFFL, 0x3FFFFFFFL, 0x7FFFFFFFL,
                              0xFFFFFFFFL};
    #define shift_left(x, n)  (((x) & setbits[32 - (n)]) << (n))
    #define shift_right(x, n) (((x) >> (n)) & setbits[32 - (n)])

    /// * Function prototypes
    //

    int             get_pck         (FILE *, WORD *);
    static void     unpack_word     (FILE *, int, int, WORD *);
    int             get_pck3        (FILE *, LONG *);
    void     		unpack_long     (FILE *, int, int, LONG *);

    // **************************************************************************
    // * Function: get_pck
    // ***************************************************************************
    int
    get_pck(FILE *fp, WORD *img)
    {
    int 	x = 0, y = 0, i = 0, c = 0;
    char 	header[BUFSIZ];

            if (fp == NULL) return 0;

            rewind( fp );
            header[0] = '\n';
            header[1] = 0;

            // Scan file until PCK header is found

            while ((c != EOF) && ((x == 0) || (y == 0))) {
                c = i = x = y = 0;

                while ((++i < BUFSIZ) && (c != EOF) && (c != '\n') && (x==0) && (y==0)) {
                    if ((header[i] = c = getc(fp)) == '\n')
                        sscanf(header, PACKIDENTIFIER, &x, &y);
            }

                unpack_word(fp, x, y, img);
        }

        return( 1 );
    }

    *****************************************************************************
    * Function: unpack_word
    * Unpacks a packed image into the WORD-array 'img'.
    *****************************************************************************
    static void
    unpack_word(FILE *packfile, int x, int y, WORD *img)
    {
    int 		valids = 0, spillbits = 0, usedbits, total = x * y;
    LONG 		window = 0L, spill, pixel = 0, nextint, bitnum, pixnum;
    static int 	bitdecode[8] = {0, 4, 5, 6, 7, 8, 16, 32};

        while (pixel < total) {
            if (valids < 6) {
                if (spillbits > 0) {
                    window |= shift_left(spill, valids);
                    valids += spillbits;
                    spillbits = 0;
            }
                    else {
                    spill = (LONG) getc(packfile);
                    spillbits = 8;
            }
            }
                else {
                pixnum = 1 << (window & setbits[3]);
                window = shift_right(window, 3);
                bitnum = bitdecode[window & setbits[3]];
                window = shift_right(window, 3);
                valids -= 6;
                while ((pixnum > 0) && (pixel < total)) {
                    if (valids < bitnum) {
                    if (spillbits > 0) {
                        window |= shift_left(spill, valids);
                            if ((32 - valids) > spillbits) {
                            valids += spillbits;
                            spillbits = 0;
                    }
                            else {
                            usedbits = 32 - valids;
                        spill = shift_right(spill, usedbits);
                        spillbits -= usedbits;
                        valids = 32;
                    }
                }
                    else {
                        spill = (LONG) getc(packfile);
                            spillbits = 8;
                }
                }
                    else {
                    --pixnum;
                if (bitnum == 0)
                            nextint = 0;
                    else {
                        nextint = window & setbits[bitnum];
                            valids -= bitnum;
                            window = shift_right(window, bitnum);
                        if ((nextint & (1 << (bitnum - 1))) != 0)
                            nextint |= ~setbits[bitnum];
                }
                    if (pixel > x) {
                        img[pixel] = (WORD) (nextint +
                                          (img[pixel-1] + img[pixel-x+1] +
                                           img[pixel-x] + img[pixel-x-1] + 2) / 4);
                            ++pixel;
                }
                    else if (pixel != 0) {
                        img[pixel] = (WORD) (img[pixel - 1] + nextint);
                            ++pixel;
                }
                    else
                            img[pixel++] = (WORD) nextint;
                }
            }
            }
        }
    }

    // ***************************************************************************
    // * Function: get_pck32
    // ***************************************************************************
    int
    get_pck32(FILE *fp, LONG *img)
    {
    int 	x = 0, y = 0, i = 0, c = 0;
    char 	header[BUFSIZ];

            if (fp == NULL)
                return 0;

            rewind( fp );
            header[0] = '\n';
            header[1] = 0;

            // Scan file until PCK header is found
            //

            while ((c != EOF) && ((x == 0) || (y == 0)))
                {
                c = i = x = y = 0;
                while ((++i < BUFSIZ) && (c != EOF) && (c != '\n') && (x==0) && (y==0))
                    {
                    if ((header[i] = c = getc(fp)) == '\n')
                        sscanf(header, PACKIDENTIFIER, &x, &y);
                    }
                unpack_long(fp, x, y,img);
                }
        return( 1 );
    }

    // *****************************************************************************
    // * Function: unpack_long
    // * Unpacks a packed image into the LONG-array 'img'. The image is stored
    // * in 'packfile'. The file should be properly positioned: the first BYTE
    // * read is assumed to be the first BYTE of the packed image.
    // *****************************************************************************
      void unpack_long(FILE *packfile, int x, int y, LONG *img)
        {
        int valids = 0, spillbits = 0, usedbits, total = x * y;
        LONG window = 0L, spill = 0, pixel = 0, nextint, bitnum, pixnum;
        static int bitdecode[8] = {0, 4, 5, 6, 7, 8, 16, 32};

        while (pixel < total)
            {
            if (valids < 6)
                {
                if (spillbits > 0)
                    {
                    window |= shift_left(spill, valids);
                    valids += spillbits;
                    spillbits = 0;
                    }
                else
                    {
                    spill = (LONG) getc(packfile);
                    spillbits = 8;
                    }
                }
            else
                {
                pixnum = 1 << (window & setbits[3]);
                window = shift_right(window, 3);
                bitnum = bitdecode[window & setbits[3]];
                window = shift_right(window, 3);
                valids -= 6;
                while ((pixnum > 0) && (pixel < total))
                    {
                    if (valids < bitnum)
                        {
                        if (spillbits > 0)
                            {
                            window |= shift_left(spill, valids);
                            if ((32 - valids) > spillbits)
                                {
                                valids += spillbits;
                                spillbits = 0;
                                }
                            else
                                {
                                usedbits = 32 - valids;
                                spill = shift_right(spill, usedbits);
                                spillbits -= usedbits;
                                valids = 32;
                                }
                            }
                        else
                            {
                            spill = (LONG) getc(packfile);
                            spillbits = 8;
                            }
                        }
                    else
                        {
                        --pixnum;
                        if (bitnum == 0)
                            nextint = 0;
                        else
                            {
                            nextint = window & setbits[bitnum];
                            valids -= bitnum;
                            window = shift_right(window, bitnum);
                            if ((nextint & (1 << (bitnum - 1))) != 0)
                                nextint |= ~setbits[bitnum];
                            }
                        if (pixel > x)
                            {
                            img[pixel] = (LONG) (nextint +
                                            (img[pixel-1] + img[pixel-x+1] +
                                           img[pixel-x] + img[pixel-x-1] + 2) / 4);
                            ++pixel;
                            }
                        else if (pixel != 0)
                            {
                            img[pixel] = (LONG) (img[pixel - 1] + nextint);
                            ++pixel;
                            }
                            else
                                img[pixel++] = (LONG) nextint;
                        }
                    }
                }
            }
        }
            */
}
