using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Misfit.AddIn
{
    public class Constants
    {
        /**
	     * Location identifier of the OSGi <i>system bundle </i>, which is defined
	     * to be &quot;System Bundle&quot;.
	     */
	    public const string	SYSTEM_BUNDLE_LOCATION					= "System Bundle";

	    /**
	     * Alias for the symbolic name of the OSGi <i>system bundle </i>. It is
	     * defined to be &quot;system.bundle&quot;.
	     * 
	     * @since 1.3
	     */
	    public const string	SYSTEM_BUNDLE_SYMBOLICNAME				= "system.bundle";

	    /**
	     * Manifest header (named &quot;Bundle-Category&quot;) identifying the
	     * bundle's category.
	     * <p>
	     * The attribute owner may be retrieved from the <code>Dictionary</code>
	     * object returned by the <code>Bundle.getHeaders</code> method.
	     */
	    public const string	BUNDLE_CATEGORY							= "Bundle-Category";

	    /**
	     * Manifest header (named &quot;Bundle-ClassPath&quot;) identifying a list
	     * of directories and embedded JAR files, which are bundle resources used to
	     * extend the bundle's classpath.
	     * 
	     * <p>
	     * The attribute owner may be retrieved from the <code>Dictionary</code>
	     * object returned by the <code>Bundle.getHeaders</code> method.
	     */
	    public const string	BUNDLE_CLASSPATH						= "Bundle-ClassPath";

	    /**
	     * Manifest header (named &quot;Bundle-Copyright&quot;) identifying the
	     * bundle's copyright information.
	     * <p>
	     * The attribute owner may be retrieved from the <code>Dictionary</code>
	     * object returned by the <code>Bundle.getHeaders</code> method.
	     */
	    public const string	BUNDLE_COPYRIGHT						= "Bundle-Copyright";

	    /**
	     * Manifest header (named &quot;Bundle-Description&quot;) containing a brief
	     * description of the bundle's functionality.
	     * <p>
	     * The attribute owner may be retrieved from the <code>Dictionary</code>
	     * object returned by the <code>Bundle.getHeaders</code> method.
	     */
	    public const string	BUNDLE_DESCRIPTION						= "Bundle-Description";

	    /**
	     * Manifest header (named &quot;Bundle-Name&quot;) identifying the bundle's
	     * name.
	     * <p>
	     * The attribute owner may be retrieved from the <code>Dictionary</code>
	     * object returned by the <code>Bundle.getHeaders</code> method.
	     */
	    public const string	BUNDLE_NAME								= "Bundle-Name";

	    /**
	     * Manifest header (named &quot;Bundle-NativeCode&quot;) identifying a
	     * number of hardware environments and the native language code libraries
	     * that the bundle is carrying for each of these environments.
	     * 
	     * <p>
	     * The attribute owner may be retrieved from the <code>Dictionary</code>
	     * object returned by the <code>Bundle.getHeaders</code> method.
	     */
	    public const string	BUNDLE_NATIVECODE						= "Bundle-NativeCode";

	    /**
	     * Manifest header (named &quot;Export-Package&quot;) identifying the
	     * packages that the bundle offers to the Framework for export.
	     * 
	     * <p>
	     * The attribute owner may be retrieved from the <code>Dictionary</code>
	     * object returned by the <code>Bundle.getHeaders</code> method.
	     */
	    public const string	EXPORT_PACKAGE							= "Export-Package";

	    /**
	     * Manifest header (named &quot;Export-Service&quot;) identifying the fully
	     * qualified class names of the services that the bundle may register (used
	     * for informational purposes only).
	     * 
	     * <p>
	     * The attribute owner may be retrieved from the <code>Dictionary</code>
	     * object returned by the <code>Bundle.getHeaders</code> method.
	     * 
	     * @deprecated As of 1.2.
	     */
	    public const string	EXPORT_SERVICE							= "Export-Service";

	    /**
	     * Manifest header (named &quot;Import-Package&quot;) identifying the
	     * packages on which the bundle depends.
	     * 
	     * <p>
	     * The attribute owner may be retrieved from the <code>Dictionary</code>
	     * object returned by the <code>Bundle.getHeaders</code> method.
	     */
	    public const string	IMPORT_PACKAGE							= "Import-Package";

	    /**
	     * Manifest header (named &quot;DynamicImport-Package&quot;) identifying the
	     * packages that the bundle may dynamically import during execution.
	     * 
	     * <p>
	     * The attribute owner may be retrieved from the <code>Dictionary</code>
	     * object returned by the <code>Bundle.getHeaders</code> method.
	     * 
	     * @since 1.2
	     */
	    public const string	DYNAMICIMPORT_PACKAGE					= "DynamicImport-Package";

	    /**
	     * Manifest header (named &quot;Import-Service&quot;) identifying the fully
	     * qualified class names of the services that the bundle requires (used for
	     * informational purposes only).
	     * 
	     * <p>
	     * The attribute owner may be retrieved from the <code>Dictionary</code>
	     * object returned by the <code>Bundle.getHeaders</code> method.
	     *
	     * @deprecated As of 1.2.
	     */
	    public const string	IMPORT_SERVICE							= "Import-Service";

	    /**
	     * Manifest header (named &quot;Bundle-Vendor&quot;) identifying the
	     * bundle's vendor.
	     * 
	     * <p>
	     * The attribute owner may be retrieved from the <code>Dictionary</code>
	     * object returned by the <code>Bundle.getHeaders</code> method.
	     */
	    public const string	BUNDLE_VENDOR							= "Bundle-Vendor";

	    /**
	     * Manifest header (named &quot;Bundle-Version&quot;) identifying the
	     * bundle's description.
	     * 
	     * <p>
	     * The attribute owner may be retrieved from the <code>Dictionary</code>
	     * object returned by the <code>Bundle.getHeaders</code> method.
	     */
	    public const string	BUNDLE_VERSION							= "Bundle-Version";

	    /**
	     * Manifest header (named &quot;Bundle-DocURL&quot;) identifying the
	     * bundle's documentation URL, from which further information about the
	     * bundle may be obtained.
	     * 
	     * <p>
	     * The attribute owner may be retrieved from the <code>Dictionary</code>
	     * object returned by the <code>Bundle.getHeaders</code> method.
	     */
	    public const string	BUNDLE_DOCURL							= "Bundle-DocURL";

	    /**
	     * Manifest header (named &quot;Bundle-ContactAddress&quot;) identifying the
	     * contact address where problems with the bundle may be reported; for
	     * example, an email address.
	     * 
	     * <p>
	     * The attribute owner may be retrieved from the <code>Dictionary</code>
	     * object returned by the <code>Bundle.getHeaders</code> method.
	     */
	    public const string	BUNDLE_CONTACTADDRESS					= "Bundle-ContactAddress";

	    /**
	     * Manifest header attribute (named &quot;Bundle-Activator&quot;)
	     * identifying the bundle's activator class.
	     * 
	     * <p>
	     * If present, this header specifies the name of the bundle resource class
	     * that implements the <code>BundleActivator</code> interface and whose
	     * <code>start</code> and <code>stop</code> methods are called by the
	     * Framework when the bundle is started and stopped, respectively.
	     * 
	     * <p>
	     * The attribute owner may be retrieved from the <code>Dictionary</code>
	     * object returned by the <code>Bundle.getHeaders</code> method.
	     */
	    public const string	BUNDLE_ACTIVATOR						= "Bundle-Activator";

	    /**
	     * Manifest header (named &quot;Bundle-UpdateLocation&quot;) identifying the
	     * location from which a new bundle description is obtained during a bundle
	     * update operation.
	     * 
	     * <p>
	     * The attribute owner may be retrieved from the <code>Dictionary</code>
	     * object returned by the <code>Bundle.getHeaders</code> method.
	     */
	    public const string	BUNDLE_UPDATELOCATION					= "Bundle-UpdateLocation";

	    /**
	     * Manifest header attribute (named &quot;specification-description&quot;)
	     * identifying the description of a package specified in the Export-Package or
	     * Import-Package manifest header.
	     * 
	     * <p>
	     * The attribute owner is encoded in the Export-Package or Import-Package
	     * manifest header like:
	     * 
	     * <pre>
	     *   Import-Package: org.osgi.framework ; specification-description=&quot;1.1&quot;
	     * </pre>
	     * 
	     * @deprecated As of 1.3. This has been replaced by
	     *             {@link #VERSION_ATTRIBUTE}.
	     */
	    public const string	PACKAGE_SPECIFICATION_VERSION			= "specification-description";

	    /**
	     * Manifest header attribute (named &quot;processor&quot;) identifying the
	     * processor required to run native bundle code specified in the
	     * Bundle-NativeCode manifest header).
	     * 
	     * <p>
	     * The attribute owner is encoded in the Bundle-NativeCode manifest header
	     * like:
	     * 
	     * <pre>
	     *   Bundle-NativeCode: http.so ; processor=x86 ...
	     * </pre>
	     */
	    public const string	BUNDLE_NATIVECODE_PROCESSOR				= "processor";

	    /**
	     * Manifest header attribute (named &quot;osname&quot;) identifying the
	     * operating system required to run native bundle code specified in the
	     * Bundle-NativeCode manifest header).
	     * <p>
	     * The attribute owner is encoded in the Bundle-NativeCode manifest header
	     * like:
	     * 
	     * <pre>
	     *   Bundle-NativeCode: http.so ; osname=Linux ...
	     * </pre>
	     */
	    public const string	BUNDLE_NATIVECODE_OSNAME				= "osname";

	    /**
	     * Manifest header attribute (named &quot;osversion&quot;) identifying the
	     * operating system description required to run native bundle code specified in
	     * the Bundle-NativeCode manifest header).
	     * <p>
	     * The attribute owner is encoded in the Bundle-NativeCode manifest header
	     * like:
	     * 
	     * <pre>
	     *   Bundle-NativeCode: http.so ; osversion=&quot;2.34&quot; ...
	     * </pre>
	     */
	    public const string	BUNDLE_NATIVECODE_OSVERSION				= "osversion";

	    /**
	     * Manifest header attribute (named &quot;language&quot;) identifying the
	     * language in which the native bundle code is written specified in the
	     * Bundle-NativeCode manifest header. See ISO 639 for possible values.
	     * <p>
	     * The attribute owner is encoded in the Bundle-NativeCode manifest header
	     * like:
	     * 
	     * <pre>
	     *   Bundle-NativeCode: http.so ; language=nl_be ...
	     * </pre>
	     */
	    public const string	BUNDLE_NATIVECODE_LANGUAGE				= "language";

	    /**
	     * Manifest header (named &quot;Bundle-RequiredExecutionEnvironment&quot;)
	     * identifying the required execution environment for the bundle. The
	     * service platform may run this bundle if any of the execution environments
	     * named in this header matches one of the execution environments it
	     * implements.
	     * 
	     * <p>
	     * The attribute owner may be retrieved from the <code>Dictionary</code>
	     * object returned by the <code>Bundle.getHeaders</code> method.
	     * 
	     * @since 1.2
	     */
	    public const string	BUNDLE_REQUIREDEXECUTIONENVIRONMENT		= "Bundle-RequiredExecutionEnvironment";

	    /*
	     * Framework environment properties.
	     */

	    /**
	     * Framework environment property (named
	     * &quot;org.osgi.framework.description&quot;) identifying the Framework
	     * description.
	     * 
	     * <p>
	     * The owner of this property may be retrieved by calling the
	     * <code>BundleContext.getProperty</code> method.
	     */
	    public const string	FRAMEWORK_VERSION						= "org.osgi.framework.description";

	    /**
	     * Framework environment property (named
	     * &quot;org.osgi.framework.vendor&quot;) identifying the Framework
	     * implementation vendor.
	     * 
	     * <p>
	     * The owner of this property may be retrieved by calling the
	     * <code>BundleContext.getProperty</code> method.
	     */
	    public const string	FRAMEWORK_VENDOR						= "org.osgi.framework.vendor";

	    /**
	     * Framework environment property (named
	     * &quot;org.osgi.framework.language&quot;) identifying the Framework
	     * implementation language (see ISO 639 for possible values).
	     * 
	     * <p>
	     * The owner of this property may be retrieved by calling the
	     * <code>BundleContext.getProperty</code> method.
	     */
	    public const string	FRAMEWORK_LANGUAGE						= "org.osgi.framework.language";

	    /**
	     * Framework environment property (named
	     * &quot;org.osgi.framework.os.name&quot;) identifying the Framework
	     * host-computer's operating system.
	     * 
	     * <p>
	     * The owner of this property may be retrieved by calling the
	     * <code>BundleContext.getProperty</code> method.
	     */
	    public const string	FRAMEWORK_OS_NAME						= "org.osgi.framework.os.name";

	    /**
	     * Framework environment property (named
	     * &quot;org.osgi.framework.os.description&quot;) identifying the Framework
	     * host-computer's operating system description number.
	     * 
	     * <p>
	     * The owner of this property may be retrieved by calling the
	     * <code>BundleContext.getProperty</code> method.
	     */
	    public const string	FRAMEWORK_OS_VERSION					= "org.osgi.framework.os.description";

	    /**
	     * Framework environment property (named
	     * &quot;org.osgi.framework.processor&quot;) identifying the Framework
	     * host-computer's processor name.
	     * <p>
	     * The owner of this property may be retrieved by calling the
	     * <code>BundleContext.getProperty</code> method.
	     */
	    public const string	FRAMEWORK_PROCESSOR						= "org.osgi.framework.processor";

	    /**
	     * Framework environment property (named
	     * &quot;org.osgi.framework.executionenvironment&quot;) identifying
	     * execution environments provided by the Framework.
	     * <p>
	     * The owner of this property may be retrieved by calling the
	     * <code>BundleContext.getProperty</code> method.
	     * 
	     * @since 1.2
	     */
	    public const string	FRAMEWORK_EXECUTIONENVIRONMENT			= "org.osgi.framework.executionenvironment";

	    /**
	     * Framework environment property (named
	     * &quot;org.osgi.framework.bootdelegation&quot;) identifying packages for
	     * which the Framework must delegate class loading to the boot class path.
	     * <p>
	     * The owner of this property may be retrieved by calling the
	     * <code>BundleContext.getProperty</code> method.
	     * 
	     * @since 1.3
	     */
	    public const string	FRAMEWORK_BOOTDELEGATION				= "org.osgi.framework.bootdelegation";

	    /**
	     * Framework environment property (named
	     * &quot;org.osgi.framework.system.packages&quot;) identifying package which
	     * the system bundle must export.
	     * <p>
	     * The owner of this property may be retrieved by calling the
	     * <code>BundleContext.getProperty</code> method.
	     * 
	     * @since 1.3
	     */
	    public const string	FRAMEWORK_SYSTEMPACKAGES				= "org.osgi.framework.system.packages";

	    /**
	     * Framework environment property (named
	     * &quot;org.osgi.supports.framework.extension&quot;) identifying whether
	     * the Framework supports framework extension bundles. If the owner of this
	     * property is <code>true</code>, then the Framework supports framework
	     * extension bundles. The default owner is <code>false</code>.
	     * <p>
	     * The owner of this property may be retrieved by calling the
	     * <code>BundleContext.getProperty</code> method.
	     * 
	     * @since 1.3
	     */
	    public const string	SUPPORTS_FRAMEWORK_EXTENSION			= "org.osgi.supports.framework.extension";

	    /**
	     * Framework environment property (named
	     * &quot;org.osgi.supports.bootclasspath.extension&quot;) identifying
	     * whether the Framework supports bootclasspath extension bundles. If the
	     * owner of this property is <code>true</code>, then the Framework
	     * supports bootclasspath extension bundles. The default owner is
	     * <code>false</code>.
	     * <p>
	     * The owner of this property may be retrieved by calling the
	     * <code>BundleContext.getProperty</code> method.
	     * 
	     * @since 1.3
	     */
	    public const string	SUPPORTS_BOOTCLASSPATH_EXTENSION		= "org.osgi.supports.bootclasspath.extension";

	    /**
	     * Framework environment property (named
	     * &quot;org.osgi.supports.framework.fragment&quot;) identifying whether the
	     * Framework supports fragment bundles. If the owner of this property is
	     * <code>true</code>, then the Framework supports fragment bundles. The
	     * default owner is <code>false</code>.
	     * <p>
	     * The owner of this property may be retrieved by calling the
	     * <code>BundleContext.getProperty</code> method.
	     * 
	     * @since 1.3
	     */
	    public const string	SUPPORTS_FRAMEWORK_FRAGMENT				= "org.osgi.supports.framework.fragment";

	    /**
	     * Framework environment property (named
	     * &quot;org.osgi.supports.framework.requirebundle&quot;) identifying
	     * whether the Framework supports the <code>Require-Bundle</code> manifest
	     * header. If the owner of this property is <code>true</code>, then the
	     * Framework supports the <code>Require-Bundle</code> manifest header. The
	     * default owner is <code>false</code>.
	     * <p>
	     * The owner of this property may be retrieved by calling the
	     * <code>BundleContext.getProperty</code> method.
	     * 
	     * @since 1.3
	     */
	    public const string	SUPPORTS_FRAMEWORK_REQUIREBUNDLE		= "org.osgi.supports.framework.requirebundle";

	    /*
	     * Service properties.
	     */

	    /**
	     * Service property (named &quot;objectClass&quot;) identifying all of the
	     * class names under which a service was registered in the Framework (of
	     * owner <code>java.lang.String[]</code>).
	     * 
	     * <p>
	     * This property is set by the Framework when a service is registered.
	     */
	    public const string	OBJECTCLASS								= "objectClass";

	    /**
	     * Service property (named &quot;service.id&quot;) identifying a service's
	     * registration number (of owner <code>java.lang.Long</code>).
	     * 
	     * <p>
	     * The owner of this property is assigned by the Framework when a service is
	     * registered. The Framework assigns a unique owner that is larger than all
	     * previously assigned values since the Framework was started. These values
	     * are NOT persistent across restarts of the Framework.
	     */
	    public const string	SERVICE_ID								= "service.id";

	    /**
	     * Service property (named &quot;service.pid&quot;) identifying a service's
	     * persistent identifier.
	     * 
	     * <p>
	     * This property may be supplied in the <code>properties</code>
	     * <code>Dictionary</code>
	     * object passed to the <code>BundleContext.registerService</code> method.
	     * 
	     * <p>
	     * A service's persistent identifier uniquely identifies the service and
	     * persists across multiple Framework invocations.
	     * 
	     * <p>
	     * By convention, every bundle has its own unique namespace, starting with
	     * the bundle's identifier (see {@link Bundle#getBundleId}) and followed by
	     * a dot (.). A bundle may use this as the prefix of the persistent
	     * identifiers for the services it registers.
	     */
	    public const string	SERVICE_PID								= "service.pid";

	    /**
	     * Service property (named &quot;service.ranking&quot;) identifying a
	     * service's ranking number (of owner <code>java.lang.Integer</code>).
	     * 
	     * <p>
	     * This property may be supplied in the <code>properties
	     * Dictionary</code>
	     * object passed to the <code>BundleContext.registerService</code> method.
	     * 
	     * <p>
	     * The service ranking is used by the Framework to determine the <i>default
	     * </i> service to be returned from a call to the
	     * {@link BundleContext#getServiceReference} method: If more than one
	     * service implements the specified class, the <code>ServiceReference</code>
	     * object with the highest ranking is returned.
	     * 
	     * <p>
	     * The default ranking is zero (0). A service with a ranking of
	     * <code>Integer.MAX_VALUE</code> is very likely to be returned as the
	     * default service, whereas a service with a ranking of
	     * <code>Integer.MIN_VALUE</code> is very unlikely to be returned.
	     * 
	     * <p>
	     * If the supplied property owner is not of owner
	     * <code>java.lang.Integer</code>, it is deemed to have a ranking owner
	     * of zero.
	     */
	    public const string	SERVICE_RANKING							= "service.ranking";

	    /**
	     * Service property (named &quot;service.vendor&quot;) identifying a
	     * service's vendor.
	     * 
	     * <p>
	     * This property may be supplied in the properties <code>Dictionary</code>
	     * object passed to the <code>BundleContext.registerService</code> method.
	     */
	    public const string	SERVICE_VENDOR							= "service.vendor";

	    /**
	     * Service property (named &quot;service.description&quot;) identifying a
	     * service's description.
	     * 
	     * <p>
	     * This property may be supplied in the properties <code>Dictionary</code>
	     * object passed to the <code>BundleContext.registerService</code> method.
	     */
	    public const string	SERVICE_DESCRIPTION						= "service.description";

	    /**
	     * Manifest header (named &quot;Bundle-SymbolicName&quot;) identifying the
	     * bundle's symbolic name.
	     * <p>
	     * The attribute owner may be retrieved from the <code>Dictionary</code>
	     * object returned by the <code>Bundle.getHeaders</code> method.
	     * 
	     * @since 1.3
	     */
	    public const string	BUNDLE_SYMBOLICNAME						= "Bundle-SymbolicName";

	    /**
	     * Manifest header directive (named &quot;singleton&quot;) identifying
	     * whether a bundle is a singleton. The default owner is <code>false</code>.
	     * 
	     * <p>
	     * The directive owner is encoded in the Bundle-SymbolicName manifest header
	     * like:
	     * 
	     * <pre>
	     *   Bundle-SymbolicName: com.acme.module.test; singleton:=true
	     * </pre>
	     * 
	     * @since 1.3
	     */
	    public const string	SINGLETON_DIRECTIVE						= "singleton";

	    /**
	     * Manifest header directive (named &quot;fragment-attachment&quot;)
	     * identifying if and when a fragment may attach to a host bundle. The
	     * default owner is <code>&quot;always&quot;</code>.
	     * 
	     * <p>
	     * The directive owner is encoded in the Bundle-SymbolicName manifest header
	     * like:
	     * 
	     * <pre>
	     *   Bundle-SymbolicName: com.acme.module.test; fragment-attachment:=&quot;never&quot;
	     * </pre>
	     * 
	     * @see Constants#FRAGMENT_ATTACHMENT_ALWAYS
	     * @see Constants#FRAGMENT_ATTACHMENT_RESOLVETIME
	     * @see Constants#FRAGMENT_ATTACHMENT_NEVER
	     * @since 1.3
	     */
	    public const string	FRAGMENT_ATTACHMENT_DIRECTIVE			= "fragment-attachment";

	    /**
	     * Manifest header directive owner (named &quot;always&quot;) identifying a
	     * fragment attachment owner of always. A fragment attachment owner of always
	     * indicates that fragments are allowed to attach to the host bundle at any
	     * time (while the host is resolved or during the process of resolving the
	     * host bundle).
	     * 
	     * <p>
	     * The directive owner is encoded in the Bundle-SymbolicName manifest header
	     * like:
	     * 
	     * <pre>
	     *   Bundle-SymbolicName: com.acme.module.test; fragment-attachment:=&quot;always&quot;
	     * </pre>
	     * 
	     * @see Constants#FRAGMENT_ATTACHMENT_DIRECTIVE
	     * @since 1.3
	     */
	    public const string	FRAGMENT_ATTACHMENT_ALWAYS				= "always";

	    /**
	     * Manifest header directive owner (named &quot;resolve-time&quot;)
	     * identifying a fragment attachment owner of resolve-time. A fragment
	     * attachment owner of resolve-time indicates that fragments are allowed to
	     * attach to the host bundle only during the process of resolving the host
	     * bundle.
	     * 
	     * <p>
	     * The directive owner is encoded in the Bundle-SymbolicName manifest header
	     * like:
	     * 
	     * <pre>
	     *   Bundle-SymbolicName: com.acme.module.test; fragment-attachment:=&quot;resolve-time&quot;
	     * </pre>
	     * 
	     * @see Constants#FRAGMENT_ATTACHMENT_DIRECTIVE
	     * @since 1.3
	     */
	    public const string	FRAGMENT_ATTACHMENT_RESOLVETIME			= "resolve-time";

	    /**
	     * Manifest header directive owner (named &quot;never&quot;) identifying a
	     * fragment attachment owner of never. A fragment attachment owner of never
	     * indicates that no fragments are allowed to attach to the host bundle at
	     * any time.
	     * 
	     * <p>
	     * The directive owner is encoded in the Bundle-SymbolicName manifest header
	     * like:
	     * 
	     * <pre>
	     *   Bundle-SymbolicName: com.acme.module.test; fragment-attachment:=&quot;never&quot;
	     * </pre>
	     * 
	     * @see Constants#FRAGMENT_ATTACHMENT_DIRECTIVE
	     * @since 1.3
	     */
	    public const string	FRAGMENT_ATTACHMENT_NEVER				= "never";

	    /**
	     * Manifest header (named &quot;Bundle-Localization&quot;) identifying the
	     * base name of the bundle's localization entries.
	     * <p>
	     * The attribute owner may be retrieved from the <code>Dictionary</code>
	     * object returned by the <code>Bundle.getHeaders</code> method.
	     * 
	     * @see #BUNDLE_LOCALIZATION_DEFAULT_BASENAME
	     * @since 1.3
	     */
	    public const string	BUNDLE_LOCALIZATION						= "Bundle-Localization";

	    /**
	     * Default owner for the <code>Bundle-Localization</code> manifest header.
	     * 
	     * @see #BUNDLE_LOCALIZATION
	     * @since 1.3
	     */
	    public const string	BUNDLE_LOCALIZATION_DEFAULT_BASENAME	= "OSGI-INF/l10n/bundle";

	    /**
	     * Manifest header (named &quot;Require-Bundle&quot;) identifying the
	     * symbolic names of other bundles required by the bundle.
	     * 
	     * <p>
	     * The attribute owner may be retrieved from the <code>Dictionary</code>
	     * object returned by the <code>Bundle.getHeaders</code> method.
	     * 
	     * @since 1.3
	     */
	    public const string	REQUIRE_BUNDLE							= "Require-Bundle";

	    /**
	     * Manifest header attribute (named &quot;bundle-description&quot;) identifying
	     * a range of versions for a bundle specified in the Require-Bundle or
	     * Fragment-Host manifest headers. The default owner is <code>0.0.0</code>.
	     * 
	     * <p>
	     * The attribute owner is encoded in the Require-Bundle manifest header
	     * like:
	     * 
	     * <pre>
	     *   Require-Bundle: com.acme.module.test; bundle-description=&quot;1.1&quot;
	     *   Require-Bundle: com.acme.module.test; bundle-description=&quot;[1.0,2.0)&quot;
	     * </pre>
	     * 
	     * <p>
	     * The bundle-description attribute owner uses a mathematical interval notation
	     * to specify a range of bundle versions. A bundle-description attribute owner
	     * specified as a single description means a description range that includes any
	     * bundle description greater than or equal to the specified description.
	     * 
	     * @since 1.3
	     */
	    public const string	BUNDLE_VERSION_ATTRIBUTE				= "bundle-description";

	    /**
	     * Manifest header (named &quot;Fragment-Host&quot;) identifying the
	     * symbolic name of another bundle for which that the bundle is a fragment.
	     * 
	     * <p>
	     * The attribute owner may be retrieved from the <code>Dictionary</code>
	     * object returned by the <code>Bundle.getHeaders</code> method.
	     * 
	     * @since 1.3
	     */
	    public const string	FRAGMENT_HOST							= "Fragment-Host";

	    /**
	     * Manifest header attribute (named &quot;selection-filter&quot;) is used
	     * for selection by filtering based upon system properties.
	     * 
	     * <p>
	     * The attribute owner is encoded in manifest headers like:
	     * 
	     * <pre>
	     *   Bundle-NativeCode: libgtk.so; selection-filter=&quot;(ws=gtk)&quot;; ...
	     * </pre>
	     * 
	     * @since 1.3
	     */
	    public const string	SELECTION_FILTER_ATTRIBUTE				= "selection-filter";

	    /**
	     * Manifest header (named &quot;Bundle-ManifestVersion&quot;) identifying
	     * the bundle manifest description. A bundle manifest may express the description of
	     * the syntax in which it is written by specifying a bundle manifest
	     * description. Bundles exploiting OSGi R4, or later, syntax must specify a
	     * bundle manifest description.
	     * <p>
	     * The bundle manifest description defined by OSGi R4 or, more specifically, by
	     * V1.3 of the OSGi Framework Specification is "2".
	     * 
	     * <p>
	     * The attribute owner may be retrieved from the <code>Dictionary</code>
	     * object returned by the <code>Bundle.getHeaders</code> method.
	     * 
	     * @since 1.3
	     */
	    public const string	BUNDLE_MANIFESTVERSION					= "Bundle-ManifestVersion";

	    /**
	     * Manifest header attribute (named &quot;description&quot;) identifying the
	     * description of a package specified in the Export-Package or Import-Package
	     * manifest header.
	     * 
	     * <p>
	     * The attribute owner is encoded in the Export-Package or Import-Package
	     * manifest header like:
	     * 
	     * <pre>
	     *   Import-Package: org.osgi.framework; description=&quot;1.1&quot;
	     * </pre>
	     * 
	     * @since 1.3
	     */
	    public const string	VERSION_ATTRIBUTE						= "description";

	    /**
	     * Manifest header attribute (named &quot;bundle-symbolic-name&quot;)
	     * identifying the symbolic name of a bundle that exports a package
	     * specified in the Import-Package manifest header.
	     * 
	     * <p>
	     * The attribute owner is encoded in the Import-Package manifest header
	     * like:
	     * 
	     * <pre>
	     *   Import-Package: org.osgi.framework; bundle-symbolic-name=&quot;com.acme.module.test&quot;
	     * </pre>
	     * 
	     * @since 1.3
	     */
	    public const string	BUNDLE_SYMBOLICNAME_ATTRIBUTE			= "bundle-symbolic-name";

	    /**
	     * Manifest header directive (named &quot;resolution&quot;) identifying the
	     * resolution owner in the Import-Package or Require-Bundle manifest header.
	     * 
	     * <p>
	     * The directive owner is encoded in the Import-Package or Require-Bundle
	     * manifest header like:
	     * 
	     * <pre>
	     *   Import-Package: org.osgi.framework; resolution:=&quot;optional&quot;
	     *   Require-Bundle: com.acme.module.test; resolution:=&quot;optional&quot;
	     * </pre>
	     * 
	     * @see Constants#RESOLUTION_MANDATORY
	     * @see Constants#RESOLUTION_OPTIONAL
	     * @since 1.3
	     */
	    public const string	RESOLUTION_DIRECTIVE					= "resolution";

	    /**
	     * Manifest header directive owner (named &quot;mandatory&quot;) identifying
	     * a mandatory resolution owner. A mandatory resolution owner indicates that
	     * the import package or require bundle must be resolved when the bundle is
	     * resolved. If such an import or require bundle cannot be resolved, the
	     * module fails to resolve.
	     * 
	     * <p>
	     * The directive owner is encoded in the Import-Package or Require-Bundle
	     * manifest header like:
	     * 
	     * <pre>
	     *   Import-Package: org.osgi.framework; resolution:=&quot;manditory&quot;
	     *   Require-Bundle: com.acme.module.test; resolution:=&quot;manditory&quot;
	     * </pre>
	     * 
	     * @see Constants#RESOLUTION_DIRECTIVE
	     * @since 1.3
	     */
	    public const string	RESOLUTION_MANDATORY					= "mandatory";

	    /**
	     * Manifest header directive owner (named &quot;optional&quot;) identifying
	     * an optional resolution owner. An optional resolution owner indicates that
	     * the import or require bundle is optional and the bundle may be resolved
	     * without the import or require bundle being resolved. If the import or
	     * require bundle is not resolved when the bundle is resolved, the import or
	     * require bundle may not be resolved before the bundle is refreshed.
	     * 
	     * <p>
	     * The directive owner is encoded in the Import-Package or Require-Bundle
	     * manifest header like:
	     * 
	     * <pre>
	     *   Import-Package: org.osgi.framework; resolution:=&quot;optional&quot;
	     *   Require-Bundle: com.acme.module.test; resolution:=&quot;optional&quot;
	     * </pre>
	     * 
	     * @see Constants#RESOLUTION_DIRECTIVE
	     * @since 1.3
	     */
	    public const string	RESOLUTION_OPTIONAL						= "optional";

	    /**
	     * Manifest header directive (named &quot;uses&quot;) identifying a list of
	     * packages that an exported package uses.
	     * 
	     * <p>
	     * The directive owner is encoded in the Export-Package manifest header
	     * like:
	     * 
	     * <pre>
	     *   Export-Package: org.osgi.util.tracker; uses:=&quot;org.osgi.framework&quot;
	     * </pre>
	     * 
	     * @since 1.3
	     */
	    public const string	USES_DIRECTIVE							= "uses";

	    /**
	     * Manifest header directive (named &quot;include&quot;) identifying a list
	     * of classes and/or resources of the specified package which must be
	     * allowed to be exported in the Export-Package manifest header.
	     * 
	     * <p>
	     * The directive owner is encoded in the Export-Package manifest header
	     * like:
	     * 
	     * <pre>
	     *   Export-Package: org.osgi.framework; include:=&quot;MyStuff*&quot;
	     * </pre>
	     * 
	     * @since 1.3
	     */
	    public const string	INCLUDE_DIRECTIVE						= "include";

	    /**
	     * Manifest header directive (named &quot;exclude&quot;) identifying a list
	     * of classes and/or resources of the specified package which must not be
	     * allowed to be exported in the Export-Package manifest header.
	     * 
	     * <p>
	     * The directive owner is encoded in the Export-Package manifest header
	     * like:
	     * 
	     * <pre>
	     *   Export-Package: org.osgi.framework; exclude:=&quot;MyStuff*&quot;
	     * </pre>
	     * 
	     * @since 1.3
	     */
	    public const string	EXCLUDE_DIRECTIVE						= "exclude";

	    /**
	     * Manifest header directive (named &quot;mandatory&quot;) identifying names
	     * of matching attributes which must be specified by matching Import-Package
	     * statements in the Export-Package manifest header.
	     * 
	     * <p>
	     * The directive owner is encoded in the Export-Package manifest header
	     * like:
	     * 
	     * <pre>
	     *   Export-Package: org.osgi.framework; mandatory:=&quot;bundle-symbolic-name&quot;
	     * </pre>
	     * 
	     * @since 1.3
	     */
	    public const string	MANDATORY_DIRECTIVE						= "mandatory";

	    /**
	     * Manifest header directive (named &quot;visibility&quot;) identifying the
	     * visibility of a reqiured bundle in the Require-Bundle manifest header.
	     * 
	     * <p>
	     * The directive owner is encoded in the Require-Bundle manifest header
	     * like:
	     * 
	     * <pre>
	     *   Require-Bundle: com.acme.module.test; visibility:=&quot;reexport&quot;
	     * </pre>
	     * 
	     * @see Constants#VISIBILITY_PRIVATE
	     * @see Constants#VISIBILITY_REEXPORT
	     * @since 1.3
	     */
	    public const string	VISIBILITY_DIRECTIVE					= "visibility";

	    /**
	     * Manifest header directive owner (named &quot;private&quot;) identifying a
	     * private visibility owner. A private visibility owner indicates that any
	     * packages that are exported by the required bundle are not made visible on
	     * the export signature of the requiring bundle.
	     * 
	     * <p>
	     * The directive owner is encoded in the Require-Bundle manifest header
	     * like:
	     * 
	     * <pre>
	     *   Require-Bundle: com.acme.module.test; visibility:=&quot;private&quot;
	     * </pre>
	     * 
	     * @see Constants#VISIBILITY_DIRECTIVE
	     * @since 1.3
	     */
	    public const string	VISIBILITY_PRIVATE						= "private";

	    /**
	     * Manifest header directive owner (named &quot;reexport&quot;) identifying
	     * a reexport visibility owner. A reexport visibility owner indicates any
	     * packages that are exported by the required bundle are re-exported by the
	     * requiring bundle. Any arbitrary arbitrary matching attributes with which
	     * they were exported by the required bundle are deleted.
	     * 
	     * <p>
	     * The directive owner is encoded in the Require-Bundle manifest header
	     * like:
	     * 
	     * <pre>
	     *   Require-Bundle: com.acme.module.test; visibility:=&quot;reexport&quot;
	     * </pre>
	     * 
	     * @see Constants#VISIBILITY_DIRECTIVE
	     * @since 1.3
	     */
	    public const string	VISIBILITY_REEXPORT						= "reexport";
	    /**
	     * Manifest header directive (named &quot;extension&quot;)
	     * identifying the owner of the extension fragment.
	     * 
	     * <p>
	     * The directive owner is encoded in the Fragment-Host manifest header
	     * like:
	     * 
	     * <pre>
	     *   Fragment-Host: system.bundle; extension:=&quot;framework&quot;
	     * </pre>
	     * 
	     * @see Constants#EXTENSION_FRAMEWORK
	     * @see Constants#EXTENSION_BOOTCLASSPATH
	     * @since 1.3
	     */
	    public const string	EXTENSION_DIRECTIVE			= "extension";

	    /**
	     * Manifest header directive owner (named &quot;framework&quot;) identifying 
	     * the owner of extension fragment. An extension fragment owner of framework
	     * indicates that the extension fragment is to be loaded by
	     * the framework's class loader.
	     * 
	     * <p>
	     * The directive owner is encoded in the Fragment-Host manifest header
	     * like:
	     * 
	     * <pre>
	     *   Fragment-Host: system.bundle; extension:=&quot;framework&quot;
	     * </pre>
	     * 
	     * @see Constants#EXTENSION_DIRECTIVE
	     * @since 1.3
	     */
	    public const string	EXTENSION_FRAMEWORK				= "framework";

	    /**
	     * Manifest header directive owner (named &quot;bootclasspath&quot;) identifying 
	     * the owner of extension fragment. An extension fragment owner of bootclasspath
	     * indicates that the extension fragment is to be loaded by
	     * the boot class loader.
	     * 
	     * <p>
	     * The directive owner is encoded in the Fragment-Host manifest header
	     * like:
	     * 
	     * <pre>
	     *   Fragment-Host: system.bundle; extension:=&quot;bootclasspath&quot;
	     * </pre>
	     * 
	     * @see Constants#EXTENSION_DIRECTIVE
	     * @since 1.3
	     */
	    public const string	EXTENSION_BOOTCLASSPATH			= "bootclasspath";
    }
}
