using System;
using System.Text;

namespace Xam.Utility.Extensions
{
	public abstract class RtfWrapper : IDisposable
	{
		protected abstract string TagOpen { get; }
		protected abstract string TagClose { get; }

		private bool m_disposedValue;
		private StringBuilder m_sb;

		public RtfWrapper( StringBuilder sb )
		{
			m_sb = sb;

			sb.Append( TagOpen );
		}

		public RtfWrapper( StringBuilder sb, object attributeParam )
		{
			m_sb = sb;

			sb.AppendFormat( TagOpen, attributeParam );
		}

		protected void Dispose( bool disposing )
		{
			if ( !m_disposedValue )
			{
				if ( disposing )
				{
					m_sb.Append( TagClose );
				}

				m_disposedValue = true;
			}
		}

		void IDisposable.Dispose()
		{
			Dispose( disposing: true );
			GC.SuppressFinalize( this );
		}
	}

	public class RtfBold : RtfWrapper
	{
		protected override string TagOpen => "<b>";

		protected override string TagClose => "</b>";

		public RtfBold( StringBuilder sb ) : base( sb ) { }
	}

	public class RtfColor : RtfWrapper
	{
		protected override string TagOpen => "<color={0}>";

		protected override string TagClose => "</color>";

		public RtfColor( StringBuilder sb, object attributeParam ) : base( sb, attributeParam ) { }
	}
}