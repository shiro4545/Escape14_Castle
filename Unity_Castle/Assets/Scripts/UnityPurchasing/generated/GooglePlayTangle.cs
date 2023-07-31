// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("0j/O/AAipjX4h5Hv+rjzFcTEaualjWmM5QqOGh6KlUfIVufCFkc1wiqbfxh+NmZm3Xzl89z2eN7GcvdS+mvYTrCCdnsj+y2ArJjslp6TjCf9s1KhES/0NJfodTFYjOzElJQO59hbVVpq2FtQWNhbW1qNanX/mhf2zPzqGkrue+fhg4ApMHhEPYsz5ndD25g9809BzTUVADEJ6vHR4mCpv3dNjkCY6yfqrquilKG/MpiZ8ZTbvd/Fe0jx77sHWFab3URn8k/TEgBRMrgHyxmpVFoAk6uMLlSGlXDcx7RkUCB5e+kw91w3QmyYec/EURvpce7iREcmFtM0a4TE/2F6K1zGscNq2Ft4aldcU3DcEtytV1tbW19aWTY0klekMk9W21hZW1pb");
        private static int[] order = new int[] { 4,3,9,11,7,11,12,10,13,11,11,12,13,13,14 };
        private static int key = 90;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
