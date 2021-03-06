/* Copyright (c) Microsoft Corporation. All rights reserved. */

// AssignmentPropertiesPage.aspx.cs
//
// Specifies Assignment Properties for creating an assignment (Create Mode), or 
// allows editing the properties of the assignment (Edit Mode).
// In addition, when an assignment is successfully created, 
// the page renders as a confirmation page. 
//

using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using Microsoft.LearningComponents;
using Microsoft.LearningComponents.Manifest;
using Microsoft.LearningComponents.Storage;
using Microsoft.LearningComponents.SharePoint;
using Microsoft.SharePoint;
using Microsoft.SharePointLearningKit;
using Microsoft.SharePointLearningKit.WebControls;
using Resources.Properties;
using Schema = Microsoft.SharePointLearningKit.Schema;
using SPControls = Microsoft.SharePoint.WebControls;
using System.Threading;
using System.IO;
using System.Configuration;

namespace Microsoft.SharePointLearningKit.ApplicationPages
{
    /// <summary>Code-behind for AssignmentProperties.aspx.</summary>
    public class AssignmentPropertiesPage : SlkAppBasePage
    {
        #region Control Declarations
#pragma warning disable 1591
        //Button controls
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "btn")]
        protected System.Web.UI.WebControls.Button btnTopOK;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "btn")]
        protected System.Web.UI.WebControls.Button btnBottomOK;

        //Label Controls
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lbl")]
        protected System.Web.UI.WebControls.Label lblTitle;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lbl")]
        protected System.Web.UI.WebControls.Label lblDescription;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lbl")]
        protected System.Web.UI.WebControls.Label lblPoints;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lbl")]
        protected System.Web.UI.WebControls.Label lblStart;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lbl")]
        protected System.Web.UI.WebControls.Label lblDue;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lbl")]
        protected System.Web.UI.WebControls.Label lblSharePointSite;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lbl")]
        protected System.Web.UI.WebControls.Label lblInstructorsHeader;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lbl")]
        protected System.Web.UI.WebControls.Label lblLearnersHeader;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lbl")]
        protected System.Web.UI.WebControls.Label lblLearnersText;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lbl")]
        protected System.Web.UI.WebControls.Label lblInstructorsText;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lbl")]
        protected System.Web.UI.WebControls.Label lblDistributeAssignmentText;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lbl")]
        protected System.Web.UI.WebControls.Label lblDistributeAssignmentHeader;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lbl")]
        protected System.Web.UI.WebControls.Label lblAssignmentPropText;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lbl")]
        protected System.Web.UI.WebControls.Label lblAssignmentPropHeader;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lbl")]
        protected System.Web.UI.WebControls.Label lblAssignmentTitle;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lbl")]
        protected System.Web.UI.WebControls.Label lblAssignmentDescription;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lbl")]
        protected System.Web.UI.WebControls.Label lblAssignmentPoints;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lbl")]
        protected System.Web.UI.WebControls.Label lblAssignmentPointsText;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lbl")]
        protected System.Web.UI.WebControls.Label lblAssignmentStart;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lbl")]
        protected System.Web.UI.WebControls.Label lblAssignmentStartText;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lbl")]
        protected System.Web.UI.WebControls.Label lblAssignmentDue;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lbl")]
        protected System.Web.UI.WebControls.Label lblAssignmentDueText;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lbl")]
        protected System.Web.UI.WebControls.Label lblAssignmentStatusText;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lbl")]
        protected System.Web.UI.WebControls.Label lblAPPConfirmWhatNext;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lbl")]
        protected System.Web.UI.WebControls.Label lblPointsPossible;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lbl")]
        protected System.Web.UI.WebControls.Label lblAutoReturnLearners;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lbl")]
        protected System.Web.UI.WebControls.Label lblShowAnswersToLearners;
        protected Label labelEmail;
        /// <summary>The control for the upload document header.</summary>
        protected System.Web.UI.WebControls.Label LabelUploadDocumentHeader { get; set; }
        /// <summary>The control for the upload document text.</summary>
        protected System.Web.UI.WebControls.Label LabelUploadDocumentText { get; set; }
        /// <summary>The table containing the upload document functionality.</summary>
        protected TableGrid TgUploadDocument { get; set; }
        /// <summary>The label for the file upload control.</summary>
        protected System.Web.UI.WebControls.Label LabelUploadDocumentName { get; set; }
        /// <summary>The file upload control.</summary>
        protected System.Web.UI.WebControls.FileUpload FileUploadDocument { get; set; }
        /// <summary>The label for the document library control.</summary>
        protected System.Web.UI.WebControls.Label LabelUploadDocumentLibrary { get; set; }
        /// <summary>The document library control.</summary>
        protected System.Web.UI.WebControls.DropDownList UploadDocumentLibraries { get; set; }
        protected TableGridRow RowPoints { get; set; }
        protected TableGridRow RowConfirmationPoints { get; set; }

        //TextBox Controls
        protected System.Web.UI.WebControls.TextBox txtTitle;
        protected System.Web.UI.WebControls.TextBox txtDescription;
        protected System.Web.UI.WebControls.TextBox txtPoints;

        //Others
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lnk")]
        protected System.Web.UI.WebControls.HyperLink lnkSharePointSite;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "lst")]
        protected System.Web.UI.WebControls.BulletedList lstNavigateBulletedList;

        //CheckBox and CheckBoxList

        protected CheckBox checkboxEmail;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "chk")]
        protected System.Web.UI.WebControls.CheckBox chkAutoReturnLearners;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "chk")]
        protected System.Web.UI.WebControls.CheckBox chkShowAnswersToLearners;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "chk")]
        protected CustomCheckBoxList chkListInstructors;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "chk")]
        protected CustomCheckBoxList chkListGroups;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "chk")]
        protected CustomCheckBoxList chkListLearners;

        //Panels
        protected System.Web.UI.WebControls.Panel panelAssignmentProperties;
        protected System.Web.UI.WebControls.Panel panelConfirmation;

        //Validation Control
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "rfv")]
        protected RequiredFieldValidator rfvAppTitle;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "ltrl")]
        protected Literal ltrlAppTitleReportText;
        protected RangeValidator rvPointsPossible;
        protected RegularExpressionValidator regexAppTitle;
        protected ValidationSummary appValidationSummary;

        //Error Banner
        protected ErrorBanner errorBanner;

        //SPControls DateTime Picker
        protected SPControls.DateTimeControl spDateTimeStart;
        protected SPControls.DateTimeControl spDateTimeDue;
#pragma warning restore 1591

        #endregion

#region PageMode enum
        /// <summary>Types of Modes that page can be rendered as. </summary>
        private enum PageMode
        {
            /// <summary>Creating a new assignment, e.g. instructor navigates to APP from Actions. </summary>
            Create = 0,

            /// <summary>Editing an assignment, e.g. instructor navigates to APP from Grading. </summary>
            Edit,

            /// <summary>User clicked OK, from either Create or Edit mode. </summary>
            PostBack,

            /// <summary>In the same server request as PostBack (after Create mode), the PageMode switches
            /// to Confirmation, indicating that we're displaying the confirmation page.</summary>
            Confirmation,

            /// <summary>If any exception occurs, the PageMode switches to Error mode.  The form is not displayed.</summary>
            Error
        };

#endregion PageMode enum

        #region Private Variables
        /// <summary>
        /// Holds Location Query String value  Location is only used in PageMode.Create -- it's an error if it's
        /// absent.  Location is the string used to uniquely identify one version of one file in a SharePoint
        /// document library.  Example:
        /// 696119ae-dd2f-42fb-b1c2-f57cef818553_671ff432-662d-4bb2-8a79-44eea19bf948_04da6346-aca8-453a-bcbf-             /// dc59877b911e_512_632872855930000000
        /// </summary>
        private SharePointFileLocation location;
        /// <summary>
        /// Holds OrgIndex Query String value.  OrgIndex is only used in PageMode.Create -- in that case, it's
        /// null for non-e-learning content.  OrgIndex is the zero-based index of the organization (i.e. root
        /// activity) of SCORM content -- it indicates which organization the user wants to attempt.
        /// </summary>
        private int? m_orgIndex;
        /// <summary>
        /// Holds assignmentID Query String value.  Only used in PageMode.Edit.  AssignmentId is the
        /// AssignmentItemIdentifier value of the assignment being edited.
        /// </summary>
        private long? m_assignmentId;
        /// <summary>
        /// Holds all Slk Members, i.e. the learners, instructors, and groups on this SPWeb.  In PageMode.Edit,
        /// this list also contains all instructors and learners on the assignment, even if they no longer have the
        /// SLK Instructor/Learner permission on the SPWeb.
        /// </summary>
        public SlkMemberships m_slkMembers;
        /// <summary>Holds the Logged in User has Instructor Persmisson in the Current SPWeb. </summary>
        private bool? m_isInstructor;
        /// <summary>Holds Current SPWeb User's SlkUser Key </summary>
        private string m_currentSlkUserKey;
        private Dictionary<AssignmentProperty, WebControl> customProperties = new Dictionary<AssignmentProperty, WebControl>();

        #endregion Private Variables

        #region Private Properties
        /// <summary>
        /// Gets the OrgIndex Query String value.  Value is null if either the query string
        /// hasn't been parsed yet, or the query string is absent.
        /// </summary>
        private int? OrgIndex
        {
            get
            {
                if (m_orgIndex == null)
                {
                    m_orgIndex = QueryString.ParseIntOptional(QueryStringKeys.OrgIndex);
                }
                return m_orgIndex;
            }
        }

        /// <summary>Gets the file location.</summary>
        private SharePointFileLocation Location
        {
            get
            {
                if (location == null)
                {
                    string locationString = QueryString.ParseString(QueryStringKeys.Location);

                    if (string.IsNullOrEmpty(locationString) == false)
                    {
                        location = new SharePointFileLocation(locationString);
                    }
                }
                return location;
            }
        }

        /// <summary>
        /// Gets the AssignmentId Query String value.  Value is null if either the query string
        /// hasn't been parsed yet, or the query string is absent.
        /// </summary>
        private long? AssignmentId
        {
            get
            {
                if (m_assignmentId == null)
                {
                    m_assignmentId = QueryString.ParseLong(QueryStringKeys.AssignmentId, -1);
                }
                return m_assignmentId == -1 ? null : m_assignmentId;
            }
        }

        /// <summary>Gets the AssignmentItemIdentifier value. </summary>
        private AssignmentItemIdentifier AssignmentItemIdentifier
        {
            get
            {
                AssignmentItemIdentifier assignmentItemId = null;
                if (AssignmentId != null)
                {
                    assignmentItemId = new AssignmentItemIdentifier(AssignmentId.Value);
                }
                return assignmentItemId;
            }
        }

        /// <summary>Gets the AssignmentProperties. </summary>
        private AssignmentProperties AssignmentProperties {get; set;}

        /// <summary>Gets/Sets the AssignmentProperties Page Mode. </summary>
        private PageMode AppMode {get; set;}

        /// <summary>Get All Slk Members: Instructors,Learners and groups </summary>
        private SlkMemberships SlkMembers
        {
            get
            {
                if (m_slkMembers == null)
                {
                    PopulateSlkMembers();
                }
                return m_slkMembers;
            }
        }

        /// <summary>Gets the Assigned Learners for this Assignment. </summary>
        private List<long> AssignedLearnersCollection {get; set;}

        /// <summary>Get Groups and associated Group Members for each group. </summary>
        private long[][] SlkGroupMemberCollection { get; set; }

        /// <summary>
        /// Return true if the Current User is Instructor in the given SPWeb or 
        /// Instructor in the Current Assignment (accessed thro in Edit Mode)
        /// </summary>
        private bool IsInstructor
        {
            get
            {
                if (m_isInstructor == null)
                {
                    m_isInstructor = false;
                    //Check if Current User is there in list of instructors 
                    //by comparing the SPWeb.CurrentUser(SPUser) with slkUser.SPUser
                    //if the slkUser.SPUser is not null then it should return the result.
                    foreach (SlkUser slkUser in SlkMembers.Instructors)
                    {
                        //Check If Current User is Instructor
                        if (slkUser.SPUser != null)
                        {
                            //Instructor List will have current userkey. 
                            //Assign it to CurrentSlkUserKey
                            if (SPWeb.CurrentUser.ID == slkUser.SPUser.ID)
                            {
                                m_isInstructor = true;
                                //set the Current SlkUser Key 
                                m_currentSlkUserKey
                                    = slkUser.UserId.GetKey().ToString(CultureInfo.InvariantCulture);
                                return true;
                            }

                        }
                    }
                    //Check if Current User is there in list of instructors 
                    //by comparing the SPWeb.CurrentUser(SPUser) UserId with slkUser UserId
                    //This Code executed only if the slkUser.SPUser is  null. Then get the CurrentUserKey 
                    //from database and compare with SlkUserkey

                    //set the Current SlkUser Key 

                    UserItemIdentifier currentSlkUser = SlkStore.CurrentUserId;
                    m_currentSlkUserKey = currentSlkUser.GetKey().ToString(CultureInfo.InvariantCulture);
                    //return True if currentSlkUserKey mathces the slkUser key in Instructor List
                    foreach (SlkUser slkUser in SlkMembers.Instructors)
                    {
                        if (currentSlkUser == slkUser.UserId)
                        {
                            m_isInstructor = true;
                            return true;
                        }
                    }
                }

                return m_isInstructor.Value;
            }
        }

        /// <summary>Current SPWeb User's SlkUser Key </summary>
        private String CurrentSlkUserKey
        {
            get
            {
                if (String.IsNullOrEmpty(m_currentSlkUserKey))
                {
                    //The GetMembership Instructor List should have the current user.
                    //Throws exception if current user is not an Instructor.
                    if (IsInstructor)
                    {
                        return m_currentSlkUserKey;
                    }
                }
                return m_currentSlkUserKey;
            }
        }

        #endregion

        #region LoadViewState
        /// <summary>LoadsViewState</summary>
        /// <param name="savedState">savedState</param>
        protected override void LoadViewState(object savedState)
        {
            if (savedState != null)
            {
                Pair p = (Pair)savedState;
                base.LoadViewState(p.First);
                Triplet appState = (Triplet)p.Second;
                AssignedLearnersCollection = new List<long>();
                //Restore the Group from ViewState
                if (appState.First != null)
                {
                    SlkGroupMemberCollection = (long[][])appState.First;
                }
                //Restore the Learner Collection from ViewState
                if (appState.Second != null)
                {
                    AssignedLearnersCollection.AddRange((long[])appState.Second);
                }
                //Restore the Current User Key from ViewState
                if (appState.Third != null)
                {
                    m_currentSlkUserKey = appState.Third.ToString();
                }
            }
        }
        #endregion

        /// <summary>See <see cref="Microsoft.SharePoint.WebControls.UnsecuredLayoutsPageBase.OnInit"/>.</summary>
        protected override void OnInit(EventArgs e)
        {
            //Setting the 24 hour mode from regional settings for start and due dates
            spDateTimeStart.HoursMode24 = SPWeb.RegionalSettings.Time24;
            spDateTimeDue.HoursMode24 = SPWeb.RegionalSettings.Time24;
            spDateTimeStart.FirstDayOfWeek = (int)SPWeb.RegionalSettings.FirstDayOfWeek;
            spDateTimeDue.FirstDayOfWeek = (int)SPWeb.RegionalSettings.FirstDayOfWeek;
            spDateTimeStart.FirstWeekOfYear = SPWeb.RegionalSettings.FirstWeekOfYear;
            spDateTimeDue.FirstWeekOfYear = SPWeb.RegionalSettings.FirstWeekOfYear;
            spDateTimeStart.LocaleId = SPWeb.Locale.LCID;
            spDateTimeDue.LocaleId = SPWeb.Locale.LCID;
            spDateTimeStart.TimeZoneID = SPWeb.RegionalSettings.TimeZone.ID;
            spDateTimeDue.TimeZoneID = SPWeb.RegionalSettings.TimeZone.ID;

            switch (SPWeb.RegionalSettings.CalendarType)
            {
                case 3: // Japanese Emperor Era
                    spDateTimeStart.Calendar = SPCalendarType.Japan;
                    spDateTimeDue.Calendar = SPCalendarType.Japan;
                    break;

                case 5: // Korean Tangun Era
                    spDateTimeStart.Calendar = SPCalendarType.Korea;
                    spDateTimeDue.Calendar = SPCalendarType.Korea;
                    break;

                case 6: // Hijri
                    spDateTimeStart.Calendar = SPCalendarType.Hijri;
                    spDateTimeDue.Calendar = SPCalendarType.Hijri;
                    break;

                case 7: // Buddhist
                    spDateTimeStart.Calendar = SPCalendarType.Thai;
                    spDateTimeDue.Calendar = SPCalendarType.Thai;
                    break;

                case 8: // Hebrew Lunar
                    spDateTimeStart.Calendar = SPCalendarType.Hebrew;
                    spDateTimeDue.Calendar = SPCalendarType.Hebrew;
                    break;

                case 9: // Gregorian Middle East French Calendar
                    spDateTimeStart.Calendar = SPCalendarType.GregorianMEFrench;
                    spDateTimeDue.Calendar = SPCalendarType.GregorianMEFrench;
                    break;

                case 10: // Gregorian Arabic Calendar
                    spDateTimeStart.Calendar = SPCalendarType.GregorianArabic;
                    spDateTimeDue.Calendar = SPCalendarType.GregorianArabic;
                    break;

                case 11: // Gregorian Transliterated English Calendar
                    spDateTimeStart.Calendar = SPCalendarType.GregorianXLITEnglish;
                    spDateTimeDue.Calendar = SPCalendarType.GregorianXLITEnglish;
                    break;

                case 12: // Gregorian Transliterated French Calendar
                    spDateTimeStart.Calendar = SPCalendarType.GregorianXLITFrench;
                    spDateTimeDue.Calendar = SPCalendarType.GregorianXLITFrench;
                    break;

                case 16: // Saka Era
                    spDateTimeStart.Calendar = SPCalendarType.SakaEra;
                    spDateTimeDue.Calendar = SPCalendarType.SakaEra;
                    break;

                case 0: // None
                case 1: // Gregorian
                default:
                    spDateTimeStart.Calendar = SPCalendarType.Gregorian;
                    spDateTimeDue.Calendar = SPCalendarType.Gregorian;
                    break;
            }

            base.OnInit(e);
        }

        #region OnPreRender
        /// <summary> Over rides OnPreRender to Render APP </summary> 
        /// <param name="e">An EventArgs that contains the event data.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            try
            {
                if (this.IsPostBack == false)
                {
                    //check for general page initialization and validation                      
                    if (AssignmentId != null)
                    {
                        AppMode = PageMode.Edit;
                        //set master page control text before any exception may occur
                        SetMasterPageControlText();
                        CheckInCorrectSite();
                    }
                    else
                    {
                        AppMode = PageMode.Create;
                        //set master page control text before any exception may occur
                        SetMasterPageControlText();
                        //If the Current User is Instructor in the given SPWeb 
                        //proceed with creating the Assignment

                        //if <packageWarnings> is not null, display the E-Learning content validation warning message
                        if (AssignmentProperties.PackageWarnings != null)
                        {
                            //Add the Package Warning in the Error Banner
                            errorBanner.AddHtmlErrorText(ErrorType.Warning, PageCulture.Resources.ActionsWarning);
                        }

                        if (AssignmentProperties.IsNoPackageAssignment)
                        {
                            SetUpForNewNoPackageAssignment();
                        }
                    }

                    SetupPageElementAttributes();
                    SetupPageValidatorAttributes();

                    if (AssignmentProperties != null)
                    {
                        //Set the Assignment Properties to UI Controls
                        DisplayAssignmentProperties();
                    }

                    panelAssignmentProperties.Visible = true;
                }
            }
            catch (ThreadAbortException)
            {
                // Calling Response.Redirect throws a ThreadAbortException which will flag an error in the next step if we don't do this.
                throw;
            }
            catch (Exception ex)
            {
                AppMode = PageMode.Error;
                WriteError(ex);
            }
        }
        #endregion

        /// <summary>See <see cref="Control.CreateChildControls"/>.</summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            Dictionary<string, int> items = new Dictionary<string, int>();

            if (AssignmentId == null)
            {
                if (Location.ToString() == Package.NoPackageLocation.ToString())
                {
                    SPWeb.Lists.ListsForCurrentUser = true;
                    int counter = 0;
                    foreach (SPList list in SPWeb.Lists)
                    {
                        if (list.BaseType == SPBaseType.DocumentLibrary && list.Hidden == false)
                        {
                            if ((list.EffectiveBasePermissions & SPBasePermissions.AddListItems) == SPBasePermissions.AddListItems)
                            {
                                items.Add(list.Title.ToUpperInvariant(), counter);
                                UploadDocumentLibraries.Items.Add(list.Title);
                                counter++;
                            }
                        }
                    }

                    QuickAssignmentSettings settings = SlkStore.Settings.QuickAssignmentSettings;

                    foreach (string defaultLibrary in settings.DefaultLibraries)
                    {
                        string upperKey = defaultLibrary.ToUpperInvariant();
                        if (items.ContainsKey(upperKey))
                        {
                            UploadDocumentLibraries.SelectedIndex = items[upperKey];
                            break;
                        }
                    }
                }
            }

            // Need to create AssignmentProperties object now in case there's any custom properties as they need adding to the page controls.
            CreateAssignmentPropertiesObject();
        }

        void SetUpForNewNoPackageAssignment()
        {
            if (UploadDocumentLibraries.Items.Count > 0)
            {
                TgUploadDocument.Visible = true;
            }
        }

        void CheckInCorrectSite()
        {
            // for the assignment site, if the user doesn't have permission to view it we'll catch the exception 
            bool previousValue = SPSecurity.CatchAccessDeniedException;
            SPSecurity.CatchAccessDeniedException = false;
            try
            {
                //Check whether the Url is directed from correct SPWeb. If not redirect to the correct SPWeb.
                if (SPWeb.ID != AssignmentProperties.SPWebGuid)
                {
                    using (SPSite spSite = new SPSite(AssignmentProperties.SPSiteGuid, SPContext.Current.Site.Zone))
                    {
                        using (SPWeb spWeb = spSite.OpenWeb(AssignmentProperties.SPWebGuid))
                        {
                            Response.Redirect(SlkUtilities.UrlCombine(spWeb.Url, Request.Path + "?" + Request.QueryString.ToString()));
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                //Catch the exception to not to restrict the user to access the content 
                //if user does not have access to SPWeb. 
                //Set the Sharepoint Site Title similar to Web Site not accessible.
                lnkSharePointSite.Text = PageCulture.Resources.APPInvalidSite;

            }
            catch (FileNotFoundException)
            {
                // Catch the exception to not to restrict the user to access the content if SPWeb does not exist.
                //Set the Sharepoint Site Title similar to Web Site not accessible.
                lnkSharePointSite.Text = PageCulture.Resources.APPInvalidSite;
            }
            finally
            {
                SPSecurity.CatchAccessDeniedException = previousValue;
            }
        }

        #region OnPreRenderComplete
        /// <summary> Overrides OnPreRenderComplete to Render APP Client Script. </summary> 
        /// <param name="e">An EventArgs that contains the event data.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        protected override void OnPreRenderComplete(EventArgs e)
        {
            try
            {
                // Set the Control Text from Resource File
                SetResourceText();

                //Render the Client Script Only when rendering APP in Create and Edit Mode 
                if (!(AppMode == PageMode.Confirmation || AppMode == PageMode.Error))
                {
                    //Register Client Script
                    RegisterGroupsClientScriptBlock();
                }
            }
            catch (Exception ex)
            {
                //Log the Exception 
                WriteError(ex);
            }
        }
        #endregion

        #region GetMemberships
        /// <summary>Constructs the lists of instructors, learners, and learner groups for the current Assignment
        /// and returns the SlkMemberships. </summary>
        private SlkMemberships GetMemberships()
        {
            // combine <instructorsByUserKey> and <additionalInstructors> (if any) into
            // <instructorsById>; we add the users from the SPWeb last, so that SharePoint user names
            // take precedence over LearningStore user names
            Dictionary<long, SlkUser> instructorsById = new Dictionary<long, SlkUser>(AssignmentProperties.Instructors.Count);
            if (AssignmentProperties.Instructors != null)
            {
                foreach (SlkUser slkUser in AssignmentProperties.Instructors)
                {
                    instructorsById[slkUser.UserId.GetKey()] = slkUser;
                }
            }

            // combine <learnersByUserKey> and <additionalLearners> (if any) into <learnersById>;
            // we add the users from the SPWeb last, so that SharePoint user names take precedence over
            // LearningStore user names
            Dictionary<long, SlkUser> learnersById = new Dictionary<long, SlkUser>(AssignmentProperties.Learners.Count + 50);
            if (AssignmentProperties.Learners != null)
            {
                foreach (SlkUser slkUser in AssignmentProperties.Learners)
                {
                    learnersById[slkUser.UserId.GetKey()] = slkUser;
                }
            }

            // convert <instructorsById> into an array, <instructors>, sorted by name
            SlkUser[] instructors = new SlkUser[instructorsById.Keys.Count];
            instructorsById.Values.CopyTo(instructors, 0);
            Array.Sort(instructors);

            // convert <learnersById> into an array, <learners>, sorted by name
            SlkUser[] learners = new SlkUser[learnersById.Keys.Count];
            learnersById.Values.CopyTo(learners, 0);
            Array.Sort(learners);

            // return a new SlkMemberships object
            return new SlkMemberships(instructors, learners, null);
        }
        #endregion

        #region BindCheckBoxItems
        /// <summary> Binds the Member Items to the given CheckBox List Control</summary>  
        /// <param name="customChkBoxList">control to bind the items</param>    
        /// <param name="slkUserCollection">Member items to bind </param>  
        /// <remarks>
        /// This method will be called once for the list of learners and once for the list
        /// of instructors.  This method is not used for learner groups checkboxes.
        /// </remarks>
        private void BindCheckBoxItems(CustomCheckBoxList customChkBoxList, ReadOnlyCollection<SlkUser> slkUserCollection)
        {

            //Adding Items to the CheckBox List Control

            //construct CheckBox List item Id IDs for checkbox controls as follows:
            //  -- "{0}" for each instructor  
            //  -- "{0}" for each learner
            //     where {0} is AssignmentMember.UserId.GetKey()
            //  -- "{0}" for each learner group
            //     where {0} is AssignmentMemberGroup.SPGroup.ID

            List<SlkCheckBoxItem> itemCollection = null;

            //Get all the learner/instructor and adds to the collection

            if (slkUserCollection != null && slkUserCollection.Count > 0)
            {
                itemCollection = new List<SlkCheckBoxItem>(slkUserCollection.Count);

                foreach (SlkUser slkUser in slkUserCollection)
                {
                    if (slkUser.SPUser != null)
                    {
                        string userName = slkUser.Name;
                        string userKey = slkUser.UserId.GetKey().ToString(CultureInfo.InvariantCulture);

                        //Create a Slk CheckBox Item
                        SlkCheckBoxItem item = new SlkCheckBoxItem(userName, userKey, false, slkUser.SPUser.LoginName);

                        //Check If Current User is Instructor
                        if (customChkBoxList.ID == chkListInstructors.ID)
                        {
                            if (CurrentSlkUserKey == userKey || (AppMode == PageMode.Create && SlkStore.Settings.SelectAllInstructors))
                            {
                                item.Selected = true;
                            }
                        }
                        else if (customChkBoxList.ID == chkListLearners.ID)
                        {
                            if (AppMode == PageMode.Create && SlkStore.Settings.SelectAllLearners)
                            {
                                item.Selected = true;
                            }
                        }

                        //If the Page Mode is Edit Check Items Added in the Create Mode
                        if (AppMode == PageMode.Edit)
                        {
                            if (customChkBoxList.ID == chkListInstructors.ID)
                            {
                                if (AssignmentProperties.Instructors.Contains(slkUser.UserId))
                                {
                                    item.Selected = true;
                                }
                            }
                            else if (customChkBoxList.ID == chkListLearners.ID)
                            {
                                if (AssignmentProperties.Learners.Contains(slkUser.UserId))
                                {
                                    item.Selected = true;
                                }
                            }
                        }

                        itemCollection.Add(item);
                    }
                }

                customChkBoxList.DataSource = itemCollection;
                customChkBoxList.DataBind();
            }
        }
        #endregion

        #region BindCheckBoxItems
        /// <summary> Binds the Member Items to the given CheckBox List Control</summary>  
        /// <param name="customChkBoxList">control to bind the items</param>    
        /// <param name="slkGroupCollection">Member items to bind </param>  
        /// <remarks>
        /// This method will be called once when the page loads, to initialize the list of
        /// learner group checkboxes.
        /// </remarks>
        private void BindCheckBoxItems(CustomCheckBoxList customChkBoxList, ReadOnlyCollection<SlkGroup> slkGroupCollection)
        {
            //Adding Items to the CheckBox List Control

            // construct CheckBox List item Id IDs for checkbox controls as follows:
            // "{0}" for each learner group
            // where {0} is AssignmentMemberGroup.SPGroup.ID
            List<SlkCheckBoxItem> itemCollection = new List<SlkCheckBoxItem>();

            int groupCount = 0;

            SlkCheckBoxItem item = new SlkCheckBoxItem(PageCulture.Resources.AppAllLearnersGroup,
                                             groupCount.ToString(CultureInfo.InvariantCulture),
                                             false, string.Empty);
            itemCollection.Add(item);

            //Get all the Groups and adds to the collection

            if (slkGroupCollection != null && slkGroupCollection.Count > 0)
            {
                foreach (SlkGroup slkGroup in slkGroupCollection)
                {
                    //If the Domain/SPGroup
                    if (slkGroup.Users.Count > 0)
                    {
                        groupCount++;
                        string groupName = slkGroup.Name;
                        string groupId = groupCount.ToString(CultureInfo.InvariantCulture);

                        StringBuilder members = new StringBuilder();

                        members.Append(groupName);
                        members.Append(Constants.Colon);

                        //To check if the Group selected 
                        bool isGroupSelected = false;
                        //If the Page Mode is Edit Check Items Added in the Create Mode.

                        if (AppMode == PageMode.Edit)
                        {
                            isGroupSelected = true;
                        }

                        //Build the Members tool Tip.                  
                        foreach (SlkUser slkUser in slkGroup.Users)
                        {
                            members.Append(Constants.Space);
                            members.Append(slkUser.Name);
                            members.Append(Constants.SemiColon);

                            //If the all the members in the group is assigned check the group.
                            if (!(AssignmentProperties.Learners.Contains(slkUser.UserId)))
                            {
                                isGroupSelected = false;
                            }
                        }
                        //Removes SemiColon at the End.
                        members.Remove(members.Length - 1, 1);

                        item = new SlkCheckBoxItem(groupName, groupId, isGroupSelected, members.ToString());
                        itemCollection.Add(item);
                    }
                }
            }

            //Bind the Collection to CheckBox list
            customChkBoxList.DataSource = itemCollection;
            customChkBoxList.DataBind();

        }
        #endregion

        #region SetResourceText
        /// <summary> Set the Control Text from Resource File</summary>   
        private void SetResourceText()
        {
            //Set the Display Text for all APP page controls from Resources
            btnBottomOK.Text = PageCulture.Resources.CtrlOKButtonText;
            btnTopOK.Text = PageCulture.Resources.CtrlOKButtonText;

            lblTitle.Text = PageCulture.Resources.CtrlLabelTitleText;
            lblDescription.Text = PageCulture.Resources.CtrlLabelDescriptionText;
            lblPoints.Text = PageCulture.Resources.CtrlLabelPointsText;
            lblStart.Text = PageCulture.Resources.CtrlLabelStartText;
            lblDue.Text = PageCulture.Resources.CtrlLabelDueText;
            lblSharePointSite.Text = PageCulture.Resources.CtrlLabelSharePointSiteText;
            lblInstructorsHeader.Text = PageCulture.Resources.CtrlLabelInstructorsText;
            lblLearnersHeader.Text = PageCulture.Resources.AppLearnersHeaderText;
            lblLearnersText.Text = PageCulture.Resources.CtrlLabelLearnerDesc;
            lblInstructorsText.Text = PageCulture.Resources.CtrlLabelInstructorDesc;
            lblDistributeAssignmentText.Text = PageCulture.Resources.CtrlLabelDistributeAssignmentDesc;
            lblDistributeAssignmentHeader.Text = PageCulture.Resources.CtrlLabelDistributeAssignmentHeader;
            LabelUploadDocumentText.Text = PageCulture.Resources.CtrlLabelUploadDocumentText;
            LabelUploadDocumentHeader.Text = PageCulture.Resources.CtrlLabelUploadDocumentHeader;
            LabelUploadDocumentName.Text = PageCulture.Resources.CtrlLabelUploadDocumentName;
            LabelUploadDocumentLibrary.Text = PageCulture.Resources.CtrlLabelUploadDocumentLibrary;

            lblPointsPossible.Text = PageCulture.Resources.CtrlLabelPointsPossible;
            lblAssignmentPropText.Text = PageCulture.Resources.CtrlLabelAssignmentPropDesc;
            lblAssignmentPropHeader.Text = PageCulture.Resources.CtrlLabelAssignmentPropHeader;

            //Show Answer and Auto Return Label Text
            lblAutoReturnLearners.Text = PageCulture.Resources.CtrlChkBoxTextAutoReturnAssignments;
            lblShowAnswersToLearners.Text = PageCulture.Resources.CtrlChkBoxTextShowAnswers;
            labelEmail.Text = PageCulture.Resources.CheckBoxEmailAssignment;

            //Confirmation Panel Label Text 
            lblAssignmentPoints.Text = PageCulture.Resources.CtrlLabelPointsText;
            lblAssignmentStart.Text = PageCulture.Resources.CtrlLabelStartText;
            lblAssignmentDue.Text = PageCulture.Resources.CtrlLabelDueText;
            lblAPPConfirmWhatNext.Text = PageCulture.Resources.CtrlLabelAPPWhatNextText;

            chkListLearners.HeaderText = PageCulture.Resources.CtrlLabelLearnersText;
            chkListGroups.HeaderText = PageCulture.Resources.CtrlLabelGroupsText;

            //Tool Tip for Calendar Controls
            spDateTimeStart.ToolTip = PageCulture.Resources.AppCalendarToolTip;
            spDateTimeDue.ToolTip = PageCulture.Resources.AppCalendarToolTip;

            //Set Server Relative Url for Start Date Picker Control 
            spDateTimeStart.DatePickerFrameUrl = SlkUtilities.UrlCombine(SPWeb.ServerRelativeUrl, spDateTimeStart.DatePickerFrameUrl);
            spDateTimeStart.DatePickerJavaScriptUrl = SlkUtilities.UrlCombine(SPWeb.ServerRelativeUrl, spDateTimeStart.DatePickerJavaScriptUrl);
            spDateTimeStart.CalendarImageUrl = SlkUtilities.UrlCombine(SPWeb.ServerRelativeUrl, spDateTimeStart.CalendarImageUrl);

            //Set Server Relative Url for Due Date Picker Control 
            spDateTimeDue.DatePickerFrameUrl = SlkUtilities.UrlCombine(SPWeb.ServerRelativeUrl, spDateTimeDue.DatePickerFrameUrl);
            spDateTimeDue.DatePickerJavaScriptUrl = SlkUtilities.UrlCombine(SPWeb.ServerRelativeUrl, spDateTimeDue.DatePickerJavaScriptUrl);
            spDateTimeDue.CalendarImageUrl = SlkUtilities.UrlCombine(SPWeb.ServerRelativeUrl, spDateTimeDue.CalendarImageUrl);

        }
        #endregion

        #region SetupPageElementAttributes
        /// <summary> Set the Page Control Element's Validation and Other Properties</summary>   
        private void SetupPageElementAttributes()
        {
            panelConfirmation.Visible = false;
            panelAssignmentProperties.Visible = false;
            lstNavigateBulletedList.DisplayMode = BulletedListDisplayMode.HyperLink;
            txtTitle.MaxLength = 1000;
            //Set Default DateTime for Start Date
            spDateTimeStart.SelectedDate = DateTime.Now.Date;

            //Add OnClientClick Event for Learners and Learners Groups and instructor.
            chkListLearners.Attributes.Add("onclick", "Slk_UpdateLearnerGroups(this)");
            chkListGroups.Attributes.Add("onclick", "Slk_UpdateLearnerGroupsGroup(this)");
            chkListInstructors.Attributes.Add("onclick", "Slk_IsCurrentUser(this)");

        }
        #endregion

        #region SetupPageValidatorAttributes
        /// <summary> Set the Page Control Elements Validation and Other Properties</summary>   
        private void SetupPageValidatorAttributes()
        {
            //Add a ValidationSummary Control in Error Banner

            //AppTitle Required Field Error 
            rfvAppTitle.ErrorMessage = PageCulture.Resources.AppTitleBlankError;
            rfvAppTitle.ControlToValidate = txtTitle.ID;
            rfvAppTitle.Display = ValidatorDisplay.Dynamic;
            rfvAppTitle.CssClass = "ms-formvalidation";

            //AppTitle Range Check Error 
            regexAppTitle.ErrorMessage = PageCulture.Resources.AppTitleMaxLengthError;
            regexAppTitle.ControlToValidate = txtTitle.ID;
            regexAppTitle.Display = ValidatorDisplay.Dynamic;
            regexAppTitle.ValidationExpression = @"[\s\S]{1,1000}";
            regexAppTitle.CssClass = "ms-formvalidation";

            //Points Possible  Check

            rvPointsPossible.ErrorMessage = PageCulture.Resources.AppPointsPossibleNaNError;
            rvPointsPossible.ControlToValidate = txtPoints.ID;
            rvPointsPossible.Type = ValidationDataType.Double;
            rvPointsPossible.Display = ValidatorDisplay.Dynamic;
            rvPointsPossible.CssClass = "ms-formvalidation";

            // Gets the NumberFormatInfo associated with the current thread culture and
            // format the Float Values

            rvPointsPossible.MinimumValue = float.MinValue.ToString(Constants.FixedPoint, NumberFormatInfo);
            rvPointsPossible.MaximumValue = float.MaxValue.ToString(Constants.FixedPoint, NumberFormatInfo);

            //Formatting appValidationSummary Control
            appValidationSummary.HeaderText = PageCulture.Resources.ErrorBannerValidationWarning;
            appValidationSummary.DisplayMode = ValidationSummaryDisplayMode.BulletList;
            appValidationSummary.CssClass = "ms-formvalidation";

            //Validaton Error Message for Learners and Instructors Group and  Enable as Required Field
            chkListLearners.ErrorMessage = PageCulture.Resources.AppLearnersRequiredError;
            chkListLearners.IsRequired = true;

            chkListInstructors.ErrorMessage = PageCulture.Resources.AppInstructorRequiredError;
            chkListInstructors.IsRequired = true;

            //Validation Error Message for Start Date and Due Date
            //You must specify a valid date within the range of 1/1/1900 and 12/31/8900.
            spDateTimeStart.MinDate = new DateTime(1900, 1, 1);
            spDateTimeStart.MaxDate = new DateTime(8900, 12, 31);
            spDateTimeStart.ErrorMessage = PageCulture.Resources.SlkNotValidDate;

            spDateTimeDue.MinDate = new DateTime(1900, 1, 1);
            spDateTimeDue.MaxDate = new DateTime(8900, 12, 31);
            spDateTimeDue.ErrorMessage = PageCulture.Resources.SlkNotValidDate;
        }
        #endregion

        #region GetMasterPageControl
        /// <summary> Returns a reference to a Label control that is in a ContentPlaceHolder control of Master Page</summary>  
        /// <param name="placeHolderID">Id of placeHolder to Find</param>
        /// <param name="literalControlID">Id of label control to be find </param>
        private Literal GetMasterPageControl(string placeHolderID, string literalControlID)
        {
            //Find the Literal Control in the MasterPage Placeholder and return
            Literal literalCtrl = null;
            ContentPlaceHolder mpContentPlaceHolder = (ContentPlaceHolder)Master.FindControl(placeHolderID);
            if (mpContentPlaceHolder != null)
            {
                literalCtrl = (Literal)mpContentPlaceHolder.FindControl(literalControlID);
            }
            return literalCtrl;
        }
        #endregion

        #region SetMasterPageControlText
        /// <summary> Set the Control Text from Resource File</summary> 
        private void SetMasterPageControlText()
        {
            Literal title = GetMasterPageControl("PlaceHolderPageTitle", "pageTitle");
            Literal titleArea = GetMasterPageControl("PlaceHolderPageTitleInTitleArea", "pageTitleinTitlePage");
            Literal pageDescription = GetMasterPageControl("PlaceHolderPageDescription", "pageDescription");

            //Set Master Page Title Controls according to APP Page Rendered Mode  
            switch (AppMode)
            {
                case PageMode.Create:
                    SetLiteralText(title, PageCulture.Resources.AppPageCreationTitle);
                    SetLiteralText(titleArea, PageCulture.Resources.AppPageCreationTitleInTitleArea);
                    SetLiteralText(pageDescription, PageCulture.Resources.AppPageCreationDescription);
                    break;
                case PageMode.Edit:
                    SetLiteralText(title, PageCulture.Resources.AppPageEditTitle);
                    SetLiteralText(titleArea, PageCulture.Resources.AppPageEditTitleInTitleArea);
                    SetLiteralText(pageDescription, PageCulture.Resources.AppPageEditDescription);
                    break;
                case PageMode.Confirmation:
                    SetLiteralText(title, PageCulture.Resources.AppPageCreateConfirmationTitle);
                    SetLiteralText(titleArea, PageCulture.Resources.AppPageCreateConfirmationTitle);
                    SetLiteralText(pageDescription, String.Empty);
                    break;
            }
        }

        void SetLiteralText(Literal literal, string value)
        {
            if (literal != null)
            {
                literal.Text = value;
            }
        }
        #endregion

        #region DisplayAssignmentProperties
        /// <summary> Sets the Assignment Properties</summary>  
        private void DisplayAssignmentProperties()
        {

            //Get Instructors,Learners and groups list and adds them to the control 
            //Member lists are stroed in following collection

            //InstructorCollection : slkMembers.Instructors;
            //LearnerCollection : slkMembers.Learners;
            //GroupCollection : slkMembers.LearnerGroups ; 

            //If No learners on SPWeb
            if (SlkMembers.Learners.Count == 0)
            {
                throw new SafeToDisplayException(PageCulture.Resources.AppLearnersNotFound);
            }

            // copy information from <assignmentProperties> to the UI
            txtTitle.Text = AssignmentProperties.Title;
            txtDescription.Text = AssignmentProperties.Description;
            txtPoints.Text = AssignmentProperties.PointsPossible == null
                             ? String.Empty
                             : AssignmentProperties.PointsPossible.Value.
                               ToString(Constants.RoundTrip, NumberFormatInfo);

            // Set the "SharePoint Site" hyperlink text and URL
            if (String.IsNullOrEmpty(lnkSharePointSite.Text))
            {
                lnkSharePointSite.Text = SPWeb.Title;
                //Target and URL for Sharepoint site
                // Set the "SharePoint Site" hyperlink Target URL to Blank
                lnkSharePointSite.Target = Constants.TargetBlank;
                lnkSharePointSite.NavigateUrl = SPWeb.ServerRelativeUrl;
                //Tool Tip for Sharepoint Site
                lnkSharePointSite.ToolTip = PageCulture.Resources.AppSharePointSiteToolTip;
            }

            //Set DateTime Start and Due Date 
            SPTimeZone timeZone = SPWeb.RegionalSettings.TimeZone;
            if (AssignmentProperties.DueDate != null)
            {
                spDateTimeDue.SelectedDate = timeZone.UTCToLocalTime(AssignmentProperties.DueDate.Value);
            }

            if (AppMode == PageMode.Edit)
            {
                spDateTimeStart.SelectedDate = timeZone.UTCToLocalTime(AssignmentProperties.StartDate);
            }

            //Add the Learners Assignment Items 

            if (AssignmentProperties.PackageFormat.HasValue)
            {
                if (AssignmentProperties.PackageFormat.Value != PackageFormat.Lrm)
                {
                    //Disabled if not class server content
                    chkShowAnswersToLearners.Enabled = false;
                    lblShowAnswersToLearners.Enabled = false;
                }
            }
            else
            {
                //Non-Elearning content. Disabled if not class server content 
                chkShowAnswersToLearners.Enabled = false;
                lblShowAnswersToLearners.Enabled = false;
            }

            chkAutoReturnLearners.Checked = AssignmentProperties.AutoReturn;

            if (AssignmentProperties.IsNonELearning)
            {
                chkAutoReturnLearners.Enabled = false;
                lblAutoReturnLearners.Enabled = false;

                if (AssignmentProperties.HidePoints)
                {
                    RowPoints.Visible = false;
                    RowConfirmationPoints.Visible = false;
                }
            }


            chkShowAnswersToLearners.Checked = AssignmentProperties.ShowAnswersToLearners;
            checkboxEmail.Checked = AssignmentProperties.EmailChanges;

            //Add Instructor List
            BindCheckBoxItems(chkListInstructors, SlkMembers.Instructors);
            //Add Learners List
            BindCheckBoxItems(chkListLearners, SlkMembers.Learners);
            //Add group List
            BindCheckBoxItems(chkListGroups, SlkMembers.LearnerGroups);

            //If the Page Mode is Edit Check Items Added in the Create Mode.
            //and add all the members assigned to the Assigned Leaners List.
            if (AppMode == PageMode.Edit)
            {
                SetAssignedLearnersCollection();
            }
            //Add all the Groups and associated members to the array.
            SetGroupMemberCollection();

        }
        #endregion

        #region GetAssignmentProperties
        /// <summary>Populates the assignment properties with the submitted values.</summary>  
        private void PopulateSubmittedAssignmentProperties()
        {
            AssignmentProperties.Title = SlkUtilities.Trim(txtTitle.Text);
            AssignmentProperties.Description = SlkUtilities.Trim(txtDescription.Text);

            string pointsPossible = SlkUtilities.Trim(txtPoints.Text);

            if (!String.IsNullOrEmpty(pointsPossible))
            {
                AssignmentProperties.PointsPossible = float.Parse(pointsPossible, NumberFormatInfo);
            }

            //Set the selected StartDate/Due Date Value
            SPTimeZone timeZone = SPWeb.RegionalSettings.TimeZone;
            AssignmentProperties.StartDate = timeZone.LocalTimeToUTC(spDateTimeStart.SelectedDate);

            if (!spDateTimeDue.IsDateEmpty)
            {
                AssignmentProperties.DueDate = timeZone.LocalTimeToUTC(spDateTimeDue.SelectedDate);
            }

            AssignmentProperties.AutoReturn = chkAutoReturnLearners.Checked;
            AssignmentProperties.ShowAnswersToLearners = chkShowAnswersToLearners.Checked;
            AssignmentProperties.EmailChanges = checkboxEmail.Checked;

            // Assign the Learners and Instructor list for assignmentProperties
            SetMembersList(AssignmentProperties.Instructors, chkListInstructors);
            SetMembersList(AssignmentProperties.Learners, chkListLearners);

            foreach (KeyValuePair<AssignmentProperty, WebControl> pair in customProperties)
            {
                ITextControl textControl = (ITextControl)pair.Value;
                pair.Key.Value = textControl.Text;
            }
        }
        #endregion

        #region SetMembersList
        /// <summary> Adds the Selected Members to the given List</summary>  
        /// <param name="slkUserCollection">list to add the members to</param>
        /// <param name="customChkBoxList">control to retrive the item from</param>
        private void SetMembersList(SlkUserCollection slkUserCollection, CustomCheckBoxList customChkBoxList)
        {
            slkUserCollection.Clear();
            if (customChkBoxList.Items.Count > 0)
            {
                //Get all the selected members and adds to the collection
                foreach (SlkCheckBoxItem item in customChkBoxList.Items)
                {
                    //Get the selected member
                    if (item.Selected)
                    {
                        //get the item key and add to collection
                        SlkUser user = SlkMembers[GetUserItemIdentifier(item.Value).GetKey()];
                        if (user == null)
                        {
                        }
                        else
                        {
                            slkUserCollection.Add(user);
                        }
                    }
                }

            }
        }
        #endregion

        #region SetAssignedLearnersCollection
        /// <summary> Adds the assigned Learners to the Assigned Learners Collection. </summary>  
        private void SetAssignedLearnersCollection()
        {
            if (AssignmentProperties != null)
            {
                if (AssignmentProperties.Learners.Count > 0)
                {
                    AssignedLearnersCollection = new List<long>(AssignmentProperties.Learners.Count);
                    foreach (SlkUser slkUser in AssignmentProperties.Learners)
                    {
                        long userKey = slkUser.UserId.GetKey();
                        //Adds the User Item to the Collection
                        if (!AssignedLearnersCollection.Contains(userKey))
                        {
                            AssignedLearnersCollection.Add(userKey);
                        }
                    }
                }
            }
        }
        #endregion

        #region SetGroupMemberCollection
        /// <summary> Adds the Group and associate Group Members to the array. </summary>  
        private void SetGroupMemberCollection()
        {
            ReadOnlyCollection<SlkGroup> slkGroupCollection = SlkMembers.LearnerGroups;

            //Populate the Group  in array
            if (slkGroupCollection != null && slkGroupCollection.Count > 0)
            {
                SlkGroupMemberCollection = new long[slkGroupCollection.Count + 1][];

                int grCount = 1;

                foreach (SlkGroup slkGroup in slkGroupCollection)
                {
                    if (slkGroup.Users.Count > 0)
                    {
                        int count = 0;

                        SlkGroupMemberCollection[grCount] = new long[slkGroup.Users.Count];

                        //Populate the Member Info in array .
                        foreach (SlkUser slkUser in slkGroup.Users)
                        {
                            long userKey = slkUser.UserId.GetKey();
                            SlkGroupMemberCollection[grCount][count] = userKey;
                            count++;
                        }
                        grCount++;
                    }
                }
            }
            else if ((slkGroupCollection != null && slkGroupCollection.Count == 0) ||
                     slkGroupCollection == null)
            {
                //To Store only All Learners Members 
                SlkGroupMemberCollection = new long[1][];
            }

            List<long> slkGroupUserList = new List<long>();
            foreach (SlkUser slkUser in SlkMembers.Learners)
            {
                long userKey = slkUser.UserId.GetKey();
                //Adds the User Item to the Collection
                if (!slkGroupUserList.Contains(userKey))
                {
                    slkGroupUserList.Add(userKey);
                }
            }

            if (slkGroupUserList.Count > 0)
            {
                //Copy all the group users  in to All Learner Index ie 0; 
                SlkGroupMemberCollection[0] = new long[slkGroupUserList.Count];
                slkGroupUserList.CopyTo(SlkGroupMemberCollection[0]);
            }
        }
        #endregion

        #region GetUserItemIdentifier
        /// <summary> Gets the userItem Identifier for the given key</summary>  
        /// <param name="userKeyValue">user key value</param>
        /// <returns> UserItemIdentifier&lt;SlkUser&gt; UserItemIdentifier </returns>
        private static UserItemIdentifier GetUserItemIdentifier(string userKeyValue)
        {
            //Get the User Identifier from Learning Components
            long userKey = long.Parse(userKeyValue, CultureInfo.InvariantCulture);
            return new UserItemIdentifier(userKey);
        }
        #endregion

        #region ValidateDateTime
        /// <summary> Validates DateTime Picker</summary>    
        /// <returns>bool</returns>
        private bool ValidateDateTime()
        {
            // Get the Startdate and due date value
            bool isDateValid = true;

            //If Start date is blank when the assignment is created, 
            //start date/time should default to the current date and the 
            //time value set to 12:00:00 midnight (00:00:00).
            if (spDateTimeStart.IsDateEmpty)
            {
                spDateTimeStart.SelectedDate = DateTime.Today;
            }

            //check for due date not empty
            if (!spDateTimeDue.IsDateEmpty)
            {
                //check for valid date time
                if (spDateTimeStart.IsValid && spDateTimeDue.IsValid)
                {
                    //check for start date is less than due date 
                    if (spDateTimeStart.SelectedDate >= spDateTimeDue.SelectedDate)
                    {
                        isDateValid = false;
                        spDateTimeDue.IsValid = false;
                        spDateTimeDue.ErrorMessage = PageCulture.Resources.AppDueDateError;
                    }
                }

            }

            return isDateValid;
        }
        #endregion

        #region VerifyPageSubmit
        /// <summary>
        /// Confirms General initialization and Validation 
        /// of the Controls in the Page is Succeeded or not.   
        /// </summary> 
        /// <returns>True if Page is Valid otherwise, false.</returns>
        protected bool VerifyPageSubmit()
        {
            //Check for DateTimeControls Validation.
            //Check for Page Validation 
            //Not setting the Page Mode as Error for rest of the processing
            //if the Page is not Valid as the Page still renders with Validation Messages
            //Setting The AppMode as Error will not process the ScriptBlock as well. 

            Page.Validate();
            bool isPageValid = ValidateDateTime() && Page.IsValid;
            return isPageValid;

        }
        #endregion

        #region SubmitAssignment
        /// <summary>
        ///  Check for Page Validation and then 
        ///  Reads the Assignment Properties Values and Submits.
        ///  In PageMode.Create, this creates an assignment; in PageMode.Edit, this
        ///  updates an existing assignment.
        /// </summary>  
        /// <param name="sender">sender</param>  
        /// <param name="e">event args</param>  
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        protected void SubmitAssignment(object sender, EventArgs e)
        {
            try
            {
                //Sets the Page Mode as PostBack 
                AppMode = PageMode.PostBack;
                //Clears the Error Banner Messages.
                errorBanner.Clear();

                //Check for Page Validation before perform any transaction.
                if (VerifyPageSubmit())
                {
                    //Get the Assignment Properties from the submitted form.
                    PopulateSubmittedAssignmentProperties();

                    if (AssignmentId == null)
                    {
                        CreateAssignment();
                    }
                    else
                    {
                        UpdateAssignment();
                    }
                }
            }
            catch (Exception ex)
            {
                //AppMode is not set to PageMode.Error as Page will be re rendered as Post Back Mode;
                //Log the Exception 
                WriteError(ex, true);
            }

        }
        #endregion

        void CreateAssignment()
        {
            try
            {
                AssignmentProperties.SetLocation(Location, OrgIndex);

                if (AssignmentProperties.IsNoPackageAssignment && FileUploadDocument.HasFile)
                {
                    SaveUploadedFile();
                }

                AssignmentProperties.Save(SPWeb, SlkRole.Instructor);
                SetConfirmationPage(AssignmentProperties.Id.GetKey());
            }
            catch (SafeToDisplayException ex)
            {
                errorBanner.Clear();
                errorBanner.AddError(ErrorType.Error, ex.Message);
                //SlkStore.DeleteAssignment(AssignmentProperties.Id);
            }
            catch (Exception ex)
            {
                //Log the Exception 
                WriteError(ex, true);
                //SlkStore.DeleteAssignment(AssignmentProperties.Id);
            }
        }

        void SaveUploadedFile()
        {
            try
            {
                SPDocumentLibrary library = SPWeb.Lists[UploadDocumentLibraries.SelectedValue] as SPDocumentLibrary;
                string destinationUrl = library.RootFolder.Url + "/" + FileUploadDocument.FileName;
                SPFile file = library.RootFolder.Files.Add(destinationUrl, FileUploadDocument.FileBytes, false);
                file.Update();

#if SP2007
                if (file.CheckOutStatus != SPFile.SPCheckOutStatus.None)
#else
                if (file.CheckOutType != SPFile.SPCheckOutType.None)
#endif
                {
                    file.CheckIn(string.Empty, SPCheckinType.MajorCheckIn);
                    file.Update();
                }

                if (file.Level != SPFileLevel.Published)
                {
                    file.Publish(string.Empty);
                    file.Update();
                }

                location = Package.CreateFileLocation(SPWeb, file);
                AssignmentProperties.SetLocation(Location, OrgIndex);
            }
            catch (SPException e)
            {
                throw new SafeToDisplayException(e.Message);
            }
        }

        #region SetConfirmationPage
        /// <summary>Sets the Assignment Properties of Confirmation Page</summary>
        /// <param name="assignmentId">Assignment Id to be Passed to Grading Page</param>
        private void SetConfirmationPage(long assignmentId)
        {
            //Set the Confirmation Page Labels

            lblAssignmentTitle.Text = SlkUtilities.GetCrlfHtmlEncodedText(AssignmentProperties.Title);
            lblAssignmentDescription.Text = SlkUtilities.ClickifyLinks(SlkUtilities.GetCrlfHtmlEncodedText(AssignmentProperties.Description));

            // say e.g.: {0:D}, {1:t} where {0} = date, {1} = time;
            lblAssignmentStartText.Text = PageCulture.Format(PageCulture.Resources.SlkDateFormatSpecifier, AssignmentProperties.StartDate.ToLocalTime());
            if (AssignmentProperties.DueDate != null)
            {
                lblAssignmentDueText.Text = PageCulture.Format(PageCulture.Resources.SlkDateFormatSpecifier, AssignmentProperties.DueDate.Value.ToLocalTime());
            }

            if (AssignmentProperties.PointsPossible != null)
            {
                lblAssignmentPointsText.Text = AssignmentProperties.PointsPossible.Value.  ToString(Constants.RoundTrip, NumberFormatInfo); }

            //Add items to the What Next list
            lstNavigateBulletedList.Items.Add(new ListItem( PageCulture.Resources.AppNavigateToSite + Constants.Space + SPWeb.Title, SPWeb.ServerRelativeUrl));

            // Get the ServerRelativeUrl for Grading Page
            string urlString = SlkUtilities.UrlCombine(SPWeb.ServerRelativeUrl, Constants.SlkUrlPath, Constants.GradingPage);
            // Append the AssignmentId QueryString key for Grading Page
            urlString = String.Format(CultureInfo.InvariantCulture,"{0}?{1}={2}", urlString, QueryStringKeys.AssignmentId, assignmentId);

            lstNavigateBulletedList.Items.Add(new ListItem( PageCulture.Resources.AppGradeOrManage, urlString));

            if (AssignmentProperties.IsNoPackageAssignment == false)
            {
                //Add Doclib Url
                SPFile spFile = Location.LoadFile();
                string name = spFile.ParentFolder.Name;
                if (string.IsNullOrEmpty(spFile.ParentFolder.Url))
                {
                    // It is the root folder so use the document library name. 
                    // This ensures that it uses the current locale version in multi-lingual sites.
                    name = spFile.ParentFolder.DocumentLibrary.Title;
                }

                string message = PageCulture.Format(PageCulture.Resources.AppNavigateToDocLib, name);
                lstNavigateBulletedList.Items.Add(new ListItem(message, spFile.ParentFolder.ServerRelativeUrl));
            }

            //Set MasterPage Controls Text for APP Confirmation Page.
            SetMasterPageControlText();

            //sets the Page Mode as Confirmation
            AppMode = PageMode.Confirmation;

            panelAssignmentProperties.Visible = false;
            panelConfirmation.Visible = true;

            //Clears all the Errors Added to the Banner.
            errorBanner.Clear();
        }
        #endregion

        #region RegisterGroupsClientScriptBlock
        /// <summary> Defines the App Learners Grouping Client Script </summary>    
        private void RegisterGroupsClientScriptBlock()
        {
            // Define the name and type of the client scripts on the page.

            String csTitle = "AppClientScript";

            Type cstype = this.GetType();

            // Get a ClientScriptManager reference from the Page class.
            ClientScriptManager cs = Page.ClientScript;
            // Check to see if the client script is already registered.
            if (!cs.IsClientScriptBlockRegistered(cstype, csTitle))
            {
                //Build the Script 
                StringBuilder csAppClientScript = new StringBuilder(1000);

                csAppClientScript.AppendLine("<!-- Place Holder App Client Script -->");

                csAppClientScript.AppendLine(
                                String.Format(CultureInfo.InvariantCulture,
                                              "slk_strLearnerGroupPrefix = \"{0}{1}\";",
                                              this.chkListGroups.ClientID,
                                              this.ClientIDSeparator));
                csAppClientScript.AppendLine(
                                String.Format(CultureInfo.InvariantCulture,
                                              "slk_strLearnerPrefix = \"{0}{1}\";",
                                              this.chkListLearners.ClientID,
                                              this.ClientIDSeparator));
                csAppClientScript.AppendLine(
                                String.Format(CultureInfo.InvariantCulture,
                                              "slk_strInstructorPrefix = \"{0}{1}\";",
                                              this.chkListInstructors.ClientID,
                                              this.ClientIDSeparator));

                //To check if the Group selected 
                bool isAllLearnersGrSelected = true;

                string slkGroups = String.Empty;

                if (SlkGroupMemberCollection != null)
                {
                    for (int i = 0; i < SlkGroupMemberCollection.Length; i++)
                    {
                        string slkGroupMembers = String.Empty;
                        //Default the First Element to null
                        slkGroupMembers = "null";
                        if (SlkGroupMemberCollection[i] != null)
                        {
                            slkGroupMembers += Constants.Comma;
                            foreach (long item in SlkGroupMemberCollection[i])
                            {
                                slkGroupMembers += item.ToString(CultureInfo.InvariantCulture) + Constants.Comma;
                                //check if all learners in the Group selected to check the 
                                //All Leaners Group Check Box
                                if (i == 0)
                                {
                                    if (AppMode == PageMode.Edit)
                                    {
                                        if (!(AssignedLearnersCollection.Contains(item)))
                                        {
                                            isAllLearnersGrSelected = false;
                                        }
                                    }
                                    else
                                    {
                                        //AllLearner Item index is zero always.
                                        isAllLearnersGrSelected = chkListGroups.Items[0].Selected;
                                    }
                                }
                            }

                            //Removes Comma at the End.
                            slkGroupMembers = slkGroupMembers.Remove(slkGroupMembers.Length - 1, 1);

                        }
                        //Register Learner Groups Array Declaration
                        cs.RegisterArrayDeclaration("slk_aLearnerGroup" + i, slkGroupMembers);
                        slkGroups += "slk_aLearnerGroup" + i + Constants.Comma;
                    }
                    slkGroups = slkGroups.Remove(slkGroups.Length - 1, 1);
                }


                //Register Learner Group Members Array Declaration 
                cs.RegisterArrayDeclaration("slk_aaLearnerGroupMembers", slkGroups);

                //Register Assigned Learner Members Array Declartion               
                //The block will executed if AppMode == PageMode.Edit and when
                //postback occurs in Edit Mode                
                if (AssignedLearnersCollection != null)
                {
                    string assignedItems = "null";
                    //check if All the learners in the Group is selected 
                    //AllLearner Item index is zero always.
                    chkListGroups.Items[0].Selected = isAllLearnersGrSelected;
                    bool isLearnerAssigned = false;
                    if (AssignedLearnersCollection.Count > 0)
                    {
                        foreach (long key in AssignedLearnersCollection)
                        {
                            assignedItems += Constants.Comma + key.ToString(CultureInfo.InvariantCulture);
                        }
                        isLearnerAssigned = true;
                    }
                    cs.RegisterArrayDeclaration("slk_aAssignedLearners", assignedItems);

                    //Register Assigned Groups Array Declartion

                    assignedItems = "null";

                    string assignedLearnersGr = String.Empty;

                    //Check if  the All Learners Group is selected. If selected add 
                    //all groups to assigned list. else loop through rest of the group
                    //and look for selected ones.
                    if (chkListGroups.Items[0].Selected)
                    {

                        foreach (SlkCheckBoxItem item in chkListGroups.Items)
                        {
                            if (item.Selected)
                            {
                                assignedLearnersGr += Constants.Comma + item.Value;
                            }
                        }
                    }
                    else
                    {
                        if (SlkGroupMemberCollection != null &&
                            SlkGroupMemberCollection.LongLength > 1)
                        {
                            //Ignore the All Learners Group and process the rest of the Groups 
                            //for each Group in SlkGroupMemberCollection look for 
                            //selected groups to assigned list.
                            for (int groupCount = 1; groupCount < SlkGroupMemberCollection.Length; groupCount++)
                            {
                                if (SlkGroupMemberCollection[groupCount] != null)
                                {
                                    if (chkListGroups.Items[groupCount].Selected)
                                    {
                                        assignedLearnersGr += Constants.Comma
                                                            + chkListGroups.Items[groupCount].Value;
                                        isLearnerAssigned = true;
                                    }
                                    else
                                    {
                                        //To check if any of the Group member is selected 
                                        for (int memberCount = 0;
                                             memberCount < SlkGroupMemberCollection[groupCount].Length;
                                             memberCount++)
                                        {
                                            //If any of the members in the group is assigned add the group to
                                            // assigned Group List.

                                            if (AssignedLearnersCollection.Contains(
                                                SlkGroupMemberCollection[groupCount][memberCount]))
                                            {
                                                assignedLearnersGr += Constants.Comma
                                                            + chkListGroups.Items[groupCount].Value;
                                                isLearnerAssigned = true;
                                                break;
                                            }

                                        }
                                    }
                                }
                            }
                        }

                    }

                    //If any Learner Assigned add the All Learners Group. 
                    if (isLearnerAssigned)
                    {
                        assignedItems += Constants.Comma
                                      + chkListGroups.Items[0].Value
                                      + assignedLearnersGr;
                    }

                    //Register Assigned Learner Groups Array Declartion
                    cs.RegisterArrayDeclaration("slk_aAssignedGroups", assignedItems);
                }
                //Construct Learner/Learner Group onclick the JavaScript events 
                //Method for toggle the status of all Learner of the Group checked/unchecked. 
                csAppClientScript.AppendLine(@"                
                function Slk_UpdateLearnerGroupsGroup(obj)
                {
                    var fCheck = document.getElementById(obj.id).checked; 
                    idGroup = obj.id.substring(slk_strLearnerGroupPrefix.length, obj.id.length);");
                //Inject the Confirmation Script if there are Assigned Learners in the Collection. 
                //Occurs when AppMode == PageMode.Edit and when Postback mode in Edit
                if (AssignedLearnersCollection != null)
                {
                    csAppClientScript.AppendLine(@"
                        if(!fCheck)
                        {
                            var isAssigned = false;
                            if(slk_aAssignedGroups != null && slk_aAssignedGroups.length > 1)
                            {
                                for (var iAssignedGr = 1; iAssignedGr < slk_aAssignedGroups.length; iAssignedGr++)
                                {
                                    if(slk_aAssignedGroups[iAssignedGr] == idGroup)
                                    {
                                        isAssigned = true;
                                        break;
                                    }
                                }
                            }                            
                        ");
                    csAppClientScript.AppendLine(@"
                           if(isAssigned)
                           {
                             var isUnAssign = confirm(""" +
                                                PageCulture.Resources.AppConfirmUnAssigning + @""");");
                    csAppClientScript.AppendLine(@"
                            if(!isUnAssign)
                            {
                                fCheck = true;
                            }                        
                           }
                        }");
                }
                //Method to check if the Learner has any assignments and 
                //to get Confirmation if the current is UnChecked from the Instructor list.

                csAppClientScript.AppendLine(@"
                    var aMembers = slk_aaLearnerGroupMembers[idGroup];
                    for (var iMember = 1; iMember < aMembers.length; iMember++)
                        document.getElementById(slk_strLearnerPrefix + aMembers[iMember]).checked = fCheck;
                    Slk_UpdateLearnerGroups(null);
                }
                function Slk_IsLearnerAssigned(id)
                {
                    if(slk_aAssignedLearners != null && slk_aAssignedLearners.length > 1)
                    {
                        for (var iAssigned = 1; iAssigned < slk_aAssignedLearners.length; iAssigned++)
                        {
                            if(slk_aAssignedLearners[iAssigned] == id)
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                }
               
                function Slk_IsCurrentUser(obj)
                {
                    var fCheck = document.getElementById(obj.id).checked;                     
                    if(!fCheck)
                    {
                        var selectedId = obj.id.substring(slk_strInstructorPrefix.length, obj.id.length);
                        var slkCurrentUserId = " + CurrentSlkUserKey + ";");

                csAppClientScript.AppendLine(@"
                        if(slkCurrentUserId == selectedId)
                        {
                            var isUnCheck = confirm(""" + PageCulture.Resources.AppConfirmUnCheckCurrentUser
                                                      + @""");");
                csAppClientScript.AppendLine(@"
                            if(!isUnCheck)
                            {
                                document.getElementById(obj.id).checked = true;
                            }   
                        }
                    }
                }");
                //Method for toggle the status of all Groups of the Learner checked/unchecked. 
                csAppClientScript.AppendLine(@"             
                function Slk_UpdateLearnerGroups(obj)
                {
                    if(obj != null)
                    {
                        var fCheck = document.getElementById(obj.id).checked; 
                        id = obj.id.substring(slk_strLearnerPrefix.length, obj.id.length);");
                //Inject the Confirmation Script if there are Assigned Learners in the Collection. 
                //Occurs when AppMode == PageMode.Edit and when Postback mode in Edit
                if (AssignedLearnersCollection != null)
                {
                    csAppClientScript.AppendLine(@"
                                if(!fCheck)
                                {
                                    var isAssigned = Slk_IsLearnerAssigned(id);
                                ");
                    csAppClientScript.AppendLine(@"
                                   if(isAssigned)
                                   {
                                     var isUnAssign = confirm("""
                                                + PageCulture.Resources.AppConfirmUnAssigning + @""");");
                    csAppClientScript.AppendLine(@"
                                    if(!isUnAssign)
                                    {
                                        document.getElementById(obj.id).checked = true;
                                    }                        
                                   }
                                }");
                }
                csAppClientScript.AppendLine(@"               
                    }
                    for (var idGroup = 0; idGroup < slk_aaLearnerGroupMembers.length; idGroup++)
                    {
                        var aMembers = slk_aaLearnerGroupMembers[idGroup];
                        var anyUnchecked = false;
                        if(aMembers != null && aMembers.length > 1)
                        {
                            for (var iMember = 1; iMember < aMembers.length; iMember++)
                            {
                                if (!document.getElementById(slk_strLearnerPrefix + aMembers[iMember]).checked)
                                {
                                    anyUnchecked = true;
                                }
                            }
                            document.getElementById(slk_strLearnerGroupPrefix + idGroup).checked = !anyUnchecked;
                        }
                        
                    }
                }                         
                    ");
                csAppClientScript.AppendLine("<!--  App Client Script Ends Here -->");

                //Register Learner/Learner Group onclick events as ClientScriptBlock
                cs.RegisterClientScriptBlock(cstype, csTitle,
                                             csAppClientScript.ToString(),
                                             true);

            }

        }
        #endregion

        #region WriteError
        /// <summary>Log the Exception Write the Standard Error in ErrorBanner and Hide the Panels</summary>  
        /// <param name="ex">Exception to be logged.</param>
        private void WriteError(Exception ex)
        {
            WriteError(ex, false);
        }
        #endregion

        #region WriteError
        /// <summary> Log the Exception Write the Standard Error in ErrorBanner and Hide the Panels</summary>  
        /// <param name="ex">Exception to be logged.</param>
        /// <param name="isAppVisible">Shows/Hides the Assignment Properties Panel</param>
        private void WriteError(Exception ex, bool isAppVisible)
        {
            //Shows/Hides the Assignment Properties Panel
            SetAppVisible(isAppVisible);
            //Enable the Error Block
            //Log the Exception 
            errorBanner.AddException(SlkStore, ex);
        }
        #endregion

        #region SetAppVisible
        /// <summary>Shows/Hides the Assignment Properties Panel and Hides the Confirmation Panels.</summary>         
        /// <param name="isAppVisible">Shows/Hides the Assignment Properties Panel</param>
        private void SetAppVisible(bool isAppVisible)
        {
            //Show/Hide Assignment Properties the Panel
            panelAssignmentProperties.Visible = isAppVisible;
            //Hide the Assignment Properties Confirmation Panel
            panelConfirmation.Visible = false;
        }
        #endregion

        #region SaveViewState
        /// <summary>SaveViewState</summary>
        /// <returns>savedObject</returns>
        protected override object SaveViewState()
        {
            //Store the base view State information
            object baseState = base.SaveViewState();
            long[] assignedLeaners = null;
            long? currentSlkUserKey = null;

            //Store the Assigned Learners and Groups information
            if (AssignedLearnersCollection != null &&
               AssignedLearnersCollection.Count > 0)
            {
                assignedLeaners = new long[AssignedLearnersCollection.Count];
                AssignedLearnersCollection.CopyTo(assignedLeaners);
            }
            //Set the CurrentSlkUserKey when rendering APP in Create and Edit Mode 
            if (!(AppMode == PageMode.Confirmation ||
                  AppMode == PageMode.Error))
            {
                currentSlkUserKey = long.Parse(CurrentSlkUserKey, CultureInfo.InvariantCulture);
            }
            object itemState
                = new Triplet(SlkGroupMemberCollection, assignedLeaners, currentSlkUserKey);
            return new Pair(baseState, itemState);
        }
        #endregion

        void UpdateAssignment()
        {

            // Get the ServerRelativeUrl for Grading Page
            string urlString = SlkUtilities.UrlCombine(SPWeb.ServerRelativeUrl, Constants.SlkUrlPath, Constants.GradingPage);
            urlString = String.Format(CultureInfo.InvariantCulture, "{0}{1}{2}={3}", urlString, Constants.QuestionMark, QueryStringKeys.AssignmentId, AssignmentId);

            try
            {
                AssignmentProperties.Save(SPWeb, SlkRole.Instructor);
                // Redirect to Grading Page
                Response.Redirect(urlString, false);

            }
            catch (Exception ex)
            {
                //Log the Exception 
                WriteError(ex, true);
                SlkStore.LogException(ex);
            }
        }

        void PopulateSlkMembers()
        {
            //Check for PageMode if it is Edit mode 
            //pass the additional Learners and Instructors 
            ReadOnlyCollection<string> groupFailures;
            string groupFailureDetails;
            if (AppMode == PageMode.Create || AppMode == PageMode.PostBack)
            {
                m_slkMembers = new SlkMemberships();
                m_slkMembers.FindAllSlkMembers(SPWeb, SlkStore, false);
                groupFailures = m_slkMembers.GroupFailures;
                groupFailureDetails = m_slkMembers.GroupFailureDetails;
            }
            else if (AppMode == PageMode.Edit)
            {

                try
                {
                    m_slkMembers = new SlkMemberships(AssignmentProperties.Instructors, AssignmentProperties.Learners, null);
                    m_slkMembers.FindAllSlkMembers(SPWeb, SlkStore, false);
                    groupFailures = m_slkMembers.GroupFailures;
                    groupFailureDetails = m_slkMembers.GroupFailureDetails;
                }
                catch (NotAnInstructorException)
                {
                    //check  whether Instructor List has the current userkey.
                    bool isInstructorInCurrentAssignment = false;
                    groupFailures = null;
                    groupFailureDetails = null;

                    //This Code executed only if the slkUser.SPUser is  null. 
                    //Get the CurrentUserKey from database and compare with SlkUserkey

                    //set the Current SlkUser Key 
                    UserItemIdentifier currentSlkUser = SlkStore.CurrentUserId;
                    m_currentSlkUserKey = currentSlkUser.GetKey().ToString(CultureInfo.InvariantCulture);
                    //return True if currentSlkUserKey mathces the slkUser key in Instructor List
                    foreach (SlkUser slkUser in AssignmentProperties.Instructors)
                    {
                        if (currentSlkUser == slkUser.UserId)
                        {
                            m_isInstructor = true;
                            m_slkMembers = GetMemberships();
                            isInstructorInCurrentAssignment = true;
                            //If Instrutor permission is removed from an SPWeb, 
                            //but that instructor is still an instructor on an assignment on that SPWeb, 
                            //then when the instructor navigates to APP (Edit Mode) 
                            //for that assignment they  see a warning 
                            SlkError slkError = new SlkError(ErrorType.Warning, PageCulture.Resources.AppNotAnInstructorInCurrentSiteError);
                            errorBanner.AddError(slkError);
                            break;
                        }
                    }

                    //if not an instructor in the current Assignment as well 
                    //throw the exception
                    if (!isInstructorInCurrentAssignment)
                    {
                        throw;
                    }
                }
            }
            else
            {
                return;
            }

            if (groupFailures != null && groupFailures.Count > 0)
            {
                //Handles <groupFailures> and <groupFailureDetails>, display the 
                //Formatted Error in Error Banner and log the failure details in event log.
                StringBuilder enumerationWarning = new StringBuilder();
                enumerationWarning.AppendLine("<ul style=\"margin-top:0;margin-bottom:0;margin-left:24;\">");
                foreach (string groupName in groupFailures)
                {
                    enumerationWarning.Append("<li>");
                    enumerationWarning.Append(Server.HtmlEncode(groupName));
                    enumerationWarning.AppendLine("</li>");
                    enumerationWarning.Append("</ul>\n");
                }
                errorBanner.AddHtmlErrorText(ErrorType.Warning, PageCulture.Format(PageCulture.Resources.AppEnumerationWarning, enumerationWarning.ToString()));
            }

            if (string.IsNullOrEmpty(groupFailureDetails) == false)
            {
                SlkStore.LogError("{0}", groupFailureDetails);
            }
        }

#region private methods
        private void CreateAssignmentPropertiesObject()
        {
            if (AssignmentId != null)
            {
                AssignmentProperties = AssignmentProperties.Load(AssignmentItemIdentifier, SlkStore);
            }
            else
            {
                if (this.IsPostBack)
                {
                    AssignmentProperties = new AssignmentProperties(null, SlkStore);
                    SlkStore.AddCustomProperties(AssignmentProperties, SPWeb);
                }
                else
                {
                    AssignmentProperties = AssignmentProperties.CreateNewAssignmentObject(SlkStore, SPWeb, SlkRole.Instructor);
                    AssignmentProperties.SetLocation(Location, OrgIndex, Request.QueryString["title"]);
                }
            }

            SetupPageCustomProperties();
        }

        private void SetupPageCustomProperties()
        {
            TableGrid table = null;

            if (AssignmentProperties.Properties.Count > 0)
            {
                table = (TableGrid)panelAssignmentProperties.FindControl("tgMainContent");
            }

            if (table != null)
            {
                int index = FindCustomPropertyIndex(table);;

                foreach (AssignmentProperty property in AssignmentProperties.Properties)
                {
                    TableGridRow row = new TableGridRow();

                    TableGridColumn labelColumn = new TableGridColumn();
                    labelColumn.ColumnType = TableGridColumn.FormType.FormLabel;
                    System.Web.UI.HtmlControls.HtmlGenericControl h3 = new System.Web.UI.HtmlControls.HtmlGenericControl("h3");
                    h3.Attributes.Add("class", "ms-standardheader");
                    labelColumn.Controls.Add(h3);
                    Label label = new Label();
                    label.ID = "custom_ID_" + property.Name;
                    label.Text = property.Title;
                    h3.Controls.Add(label);
                    row.Cells.Add(labelColumn);

                    TableGridColumn inputColumn = new TableGridColumn();
                    inputColumn.ColumnType = TableGridColumn.FormType.FormBody;
                    AddCustomPropertyControls(inputColumn, property);
                    row.Cells.Add(inputColumn);

                    table.Rows.AddAt(index, row);
                    index++;
                }
            }
        }

        private int FindCustomPropertyIndex(TableGrid table)
        {
            Control parent = checkboxEmail.Parent;
            TableGridRow emailRow = null;

            while (parent != null)
            {
                emailRow = parent as TableGridRow;
                if (emailRow != null)
                {
                    break;
                }

                parent = parent.Parent;
            }

            if (emailRow != null)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i] == emailRow)
                    {
                        return i;
                    }
                }
            }

            // Not found, add at end
            return table.Rows.Count;
        }

        private void AddCustomPropertyControls(TableGridColumn column, AssignmentProperty property)
        {
            WebControl control = null;
            TextBox text;
            CustomValidator customValidator = null;

            switch (property.Type)
            {
                case AssignmentPropertyType.Text:
                    text = new TextBox();
                    TextAssignmentProperty textProperty = (TextAssignmentProperty) property;
                    text.Text = property.Value;
                    if (textProperty.IsMultiLine)
                    {
                        text.TextMode = TextBoxMode.MultiLine;
                        text.Style.Add("overflow", "visible");
                        text.Style.Add("height", "40px");
                    }

                    control = text;
                    break;

                case AssignmentPropertyType.Url:
                    text = new TextBox();
                    text.Text = property.Value;
                    control = text;
                    customValidator = CreateUrlValidator(property);
                    break;

                case AssignmentPropertyType.Choice:
                    control = CreateCustomChoiceControl(property);
                    break;
            }

            if (control != null)
            {
                control.ID = "custom_" + property.Name;
                control.CssClass = "ms-long";
                control.Style["width"] = "98%";

                column.Controls.Add(control);

                customProperties.Add(property, control);
            }

            if (property.Required || customValidator != null)
            {
                System.Web.UI.HtmlControls.HtmlGenericControl div = new System.Web.UI.HtmlControls.HtmlGenericControl("div");
                column.Controls.Add(div);

                if (property.Required)
                {
                    div.Controls.Add(CreateRequiredValidator(property, control));
                }

                if (customValidator != null)
                {
                    customValidator.ControlToValidate = control.ID;
                    div.Controls.Add(customValidator);
                }
            }
        }

        private CustomValidator CreateUrlValidator(AssignmentProperty property)
        {
            CustomValidator customValidator = new CustomValidator();
            customValidator.ID = "custom_url_" + property.Name;
            customValidator.ServerValidate += ValidateUrl;
            customValidator.ErrorMessage = PageCulture.Format(PageCulture.Resources.AssignmentPropertiesUrlProperty, property.Title);
            customValidator.Display = ValidatorDisplay.Dynamic;
            customValidator.CssClass = "ms-formvalidation";
            return customValidator;
        }

        private void ValidateUrl(object source, ServerValidateEventArgs args)
        {
            string value = args.Value;
            string id = ((CustomValidator)source).ControlToValidate;
            if (string.IsNullOrEmpty(value) == false)
            {
                args.IsValid = Uri.IsWellFormedUriString(value, UriKind.RelativeOrAbsolute);
            }
            else
            {
                args.IsValid = true;
            }
        }

        private static DropDownList CreateCustomChoiceControl(AssignmentProperty property)
        {
            DropDownList list = new DropDownList();
            ChoiceAssignmentProperty choiceProperty = (ChoiceAssignmentProperty) property;

            if (choiceProperty.Required == false)
            {
                list.Items.Add(string.Empty);
            }

            foreach (string choice in choiceProperty.Choices)
            {
                list.Items.Add(choice);
            }

            for (int i = 0; i < list.Items.Count; i++)
            {
                if (list.Items[i].Value == choiceProperty.Value)
                {
                    list.SelectedIndex = i;
                    break;
                }
            }

            return list;
        }

        private RequiredFieldValidator CreateRequiredValidator(AssignmentProperty property, WebControl control)
        {
            RequiredFieldValidator required = new RequiredFieldValidator();
            required.ID = "custom_required_" + property.Name;
            required.ErrorMessage = PageCulture.Format(PageCulture.Resources.AssignmentPropertiesRequiredProperty, property.Title);
            required.ControlToValidate = control.ID;
            required.Display = ValidatorDisplay.Dynamic;
            required.CssClass = "ms-formvalidation";
            return required;
        }
#endregion private methods
    }
}

