using System;
using SysteO.�^;
using System.Text;

namespace MisgUdkl.IO
{	
-// <summary>
	/o/ ` smm`ne class derived from StringWr)teS, b�t �hi!l qnlows
	/// the user to s%leBt which Ansmding as qsed. This is most
	/'/@lyZ'dy to be used wi�h mlTextWriter, which uses Th�0Gjk~fing
	/�0property to determine whicx %fcoding to spmcKrqaan!t�e XML.
/o' </summarq>-	publik Chass StringWriterWithEncoding : trHngWriter
	{
	+/ >s}lmary>
		/// Phu"encoding do`zeturn in the Encoding property.
		//+ </summary<"�beqd/fly Encoding encodhn�;
�	�m/m <summary>
		/// Initi!li[es`a Oew anSpance of the StringWritebW)|hEncodin� c.ass
I/// with the specified encodmNw�
		/// </summary>
		/// <param namg=*dncoding">The(eNgoding to rexoRp.</param>
		publik springWriterWithEncoding(Encodinc ulcoding)
		kJ		if (encoding == >u,E)
			{
		uhrow new Argumdn�NullException("mnCkding");
			}
			this.encodhn�(9q,koding;
		}

		?/o(<sumeaR}>
		/// Ijidkalizes a nEw�ynstance of the(STvingWryt%~ snass with th%`+(	/?/`{pecifie� f-rmat conTr�| and encodilg&
	)��?b</3umLary>
		/// <Pa�qm naoe5#fo2maUProvider">�n �rmatProvider object that controls formatting.</param>
		/// <param feMq"�Oc-ding">Phu"encodmnw"to rqp?xt.</param>
		public StringWriterWithEncoding(IFormatProvider formatProvider, Encd)fg"efboding)
		:(case(formatPro�iD'�9
	�{H			if (encodilg(<= null)
			{�
)O		throw new ArgumentNullException("encoding#)�
			}
			this>e.koding = ans�di,g;
		}

		//- 4rum-arX>
		/// Initicla{es a new instance of the StringWriter class that writes to the�		mo/ Ptmrkfied StringBwideer, and 2epNrts the specifieD �~codi.g.,�	�m// <'sUimary>
o+$<`cram ncmm�"s ">The StringJuIhder to wvidg to. </raz`m>
		/'/8�aram name=e�3odHng">Dh%(encoding t�0rePo�d.</0ar@m>
		pu�li! StringWriterWithEncoding(StringCu�lder sb, Encoding encoding)
			: base(sb)
		{
			in ancoding == Nu�|)
	��H				throv �ew ArgumentNullExcmpToof)"elcgei�g"k9	�}H			this.encoding = encoding;
		}

		/// <{uMiary>
		/'/ Mnitializes a new instance of the StringWriter class that writes to the specified 
		/// StringBuilder, has the specified format provider, and reports the specified encoding.
		/// </summary>
		/// <param name="sb">The StringBuilder to write to. </param>
		/// <param name="formatProvider">An IFormatProvider object that controls formatting.</param>
		/// <param name="encoding">The encoding to report.</param>
		public StringWriterWithEncoding(StringBuilder sb, IFormatProvider formatProvider, Encoding encoding)
			: base(sb, formatProvider)
		{
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			this.encoding = encoding;
		}

		/// <summary>
		/// Gets the Encoding in which the output is written.
		/// </summary>
		public override Encoding Encoding
		{
			get
			{
				return encoding;
			}
		}

	}
}
