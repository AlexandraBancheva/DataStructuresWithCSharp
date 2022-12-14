using TripAdministrations;
using NUnit.Framework;
using System.Diagnostics;
using System.Linq;
using System;

class Test_01
{
    private TripAdministrator tripAdministrations;

    private Company c1 = new Company("a", 2);
    private Company c2 = new Company("b", 1);
    private Company c3 = new Company("c", 1);
    private Company c4 = new Company("d", 2);

    private Trip t1 = new Trip("a", 1, Transportation.NONE, 1);
    private Trip t2 = new Trip("b", 1, Transportation.BUS, 1);
    private Trip t3 = new Trip("c", 1, Transportation.BUS, 1);
    private Trip t4 = new Trip("d", 1, Transportation.BUS, 1);
    private Trip t5 = new Trip("e", 2, Transportation.PLANE, 120);
    private Trip t6 = new Trip("f", 5, Transportation.PLANE, 130);

    [SetUp]
    public void Setup()
    {
        this.tripAdministrations = new TripAdministrator();
    }

    [Test]
    public void TestAddCompany()
    {
        this.tripAdministrations.AddCompany(c1);
        Assert.True(this.tripAdministrations.Exist(c1));
    }

        [Test]
    public void TestExistForNotAddedCompany()
    {
        this.tripAdministrations.AddCompany(c2);
        Assert.False(this.tripAdministrations.Exist(c1));
    }

        [Test]
    public void TestExistTripForNotAddedTrip()
    {
        Assert.False(this.tripAdministrations.Exist(t1));
    }

        [Test]
    public void TestExistTripForAdded()
    {
        this.tripAdministrations.AddCompany(c1);
        this.tripAdministrations.AddTrip(c1, t1);
        Assert.True(this.tripAdministrations.Exist(t1));
    }

    [Test]
    public void TestAddCompanyPerf()
    {
        for (int i = 0; i < 10000; i++)
        {
            this.tripAdministrations.AddCompany(new Company(i.ToString(), i));
        }

        Stopwatch sw = new Stopwatch();
        sw.Start();
        this.tripAdministrations.AddCompany(c1);
        sw.Stop();
        Assert.IsTrue(sw.ElapsedMilliseconds <= 20);
    }

    // My tests
    [Test]
    public void TestIfGetCompaniesWorksCorrectly()
    {
        this.tripAdministrations.AddCompany(c1);
        this.tripAdministrations.AddCompany(c2);
        this.tripAdministrations.AddCompany(c3);
        var actual = 3;
        var expected = this.tripAdministrations.GetCompanies().Count();

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void TestIfGetTripsWorksCorrectly()
    {
        this.tripAdministrations.AddCompany(c1);
        this.tripAdministrations.AddTrip(c1, t1);

        var actual = 1;
        var expected = this.tripAdministrations.GetTrips().Count();

        Assert.AreEqual(expected, actual);
    }

    [Test]
    public void TestIfRemoveCompanyThrowsArgumentExceptionWhenTheCompanyUnexists()
    {
        Assert.Throws<ArgumentException>(() => this.tripAdministrations.RemoveCompany(c1));
    }


    [Test]
    public void TestIfRemoveCompanyWorksCorrectly()
    {
        this.tripAdministrations.AddCompany(c1);
        this.tripAdministrations.AddTrip(c1, t1);

        this.tripAdministrations.RemoveCompany(c1);

        Assert.True(!this.tripAdministrations.Exist(c1));
    }

    [Test]
    public void TestIfExecuteTripThrowsArgumentExceptionWhenTheTripDoesntExist()
    {
        this.tripAdministrations.AddCompany(c1);
        this.tripAdministrations.AddTrip(c1, t1);

        Assert.Throws<ArgumentException>(() => this.tripAdministrations.ExecuteTrip(c1, t2));
    }

    [Test]
    public void TestIfExecuteTripThrowsArgumentExceptionWhenTheCompanyDoesntExist()
    {
        this.tripAdministrations.AddCompany(c1);
        this.tripAdministrations.AddTrip(c1, t1);

        Assert.Throws<ArgumentException>(() => this.tripAdministrations.ExecuteTrip(c2, t1));
    }

    [Test]
    public void TestIfExecuteTripThrowsArgumentExceptionWhenTheCompanyDoesntContainTheTrip()
    {
        this.tripAdministrations.AddCompany(c1);
        this.tripAdministrations.AddTrip(c1, t1);

        Assert.Throws<ArgumentException>(() => this.tripAdministrations.ExecuteTrip(c1, t3));
    }

    [Test]
    public void TestIfExecuteTripWorksCorrectly()
    {
        this.tripAdministrations.AddCompany(c1);
        this.tripAdministrations.AddTrip(c1, t1);
        this.tripAdministrations.AddTrip(c1, t2);
        this.tripAdministrations.AddTrip(c1, t3);

        this.tripAdministrations.ExecuteTrip(c1, t3);
        Assert.True(!this.tripAdministrations.Exist(t3));
        Assert.False(this.tripAdministrations.Exist(t3));
    }

    [Test]
    public void TestIfGetCompaniesWithMoreThatNTrips()
    {
        this.tripAdministrations.AddCompany(c1);
        this.tripAdministrations.AddTrip(c1, t1);

        this.tripAdministrations.AddCompany(c2);
        this.tripAdministrations.AddTrip(c2, t2);
        this.tripAdministrations.AddTrip(c2, t3);
        this.tripAdministrations.AddTrip(c2, t4);

        var expected = this.tripAdministrations.GetCompaniesWithMoreThatNTrips(2).Count();

        Assert.AreEqual(expected, 1);
    }

    [Test]
    public void TestIfGetCompaniesWithMoreThatNTripsShouldReturnEmptyCollection()
    {
        this.tripAdministrations.AddCompany(c1);
        this.tripAdministrations.AddTrip(c1, t1);

        this.tripAdministrations.AddCompany(c2);
        this.tripAdministrations.AddTrip(c2, t2);

        var expected = this.tripAdministrations.GetCompaniesWithMoreThatNTrips(3).Count();
        Assert.AreEqual(expected, 0);
    }

    [Test]
    public void TestIfGetTripsWithTransportationType()
    {
        this.tripAdministrations.AddCompany(c2);
        this.tripAdministrations.AddTrip(c2, t1);
        this.tripAdministrations.AddTrip(c2, t2);
        this.tripAdministrations.AddTrip(c2, t3);
        this.tripAdministrations.AddTrip(c2, t4);

        var expected = this.tripAdministrations.GetTripsWithTransportationType(Transportation.BUS).Count();

        Assert.AreEqual(expected, 3);
    }

    [Test]
    public void TestIfGetAllTripsInPriceRangeWorksCorrectly()
    {
        this.tripAdministrations.AddCompany(c1);
        this.tripAdministrations.AddTrip(c1, t1);
        this.tripAdministrations.AddTrip(c1, t5);
        this.tripAdministrations.AddTrip(c1, t6);

        var expected = this.tripAdministrations.GetAllTripsInPriceRange(50, 150).Count();

        Assert.AreEqual(expected, 2);
    }
}