using Xunit;

namespace lw_3.TestCar

{
    public class EngineUnitTests
    {
        [Fact]
        public void Engine_wont_off_when_off()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.TurnOffEngine();
            bool isTurnoff = car.TurnOffEngine();
            Assert.False(isTurnoff);
            Assert.True(!car.IsTurnedOn());
        }

        [Fact]
        public void Engine_wont_off_not_standing()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.SetGear(1);
            car.SetSpeed(30);
            bool isTurnOff = car.TurnOffEngine();
            Assert.False(isTurnOff);
        }

        [Fact]
        public void Engine_wont_off_gear_on()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.SetGear(1);
            bool isTurnOff = car.TurnOffEngine();
            Assert.False(isTurnOff);
        }

        [Fact]
        public void Engine_off_when_on()
        {
            Car car = new Car();
            car.TurnOnEngine();
            bool isTurnOff = car.TurnOffEngine();
            Assert.True(isTurnOff);
        }

        [Fact]
        public void Engine_wont_on_when_on()
        {
            Car car = new Car();
            car.TurnOnEngine();
            bool isTurnOff = car.TurnOnEngine();
            Assert.False(isTurnOff);
            Assert.True(car.IsTurnedOn());
        }
    }

    public class GearUnitTest
    {
        [Fact]
        public void No_backwards_after_going_forward()
        {
            Car car = new Car();
            car.TurnOnEngine();
            bool isSetGear = car.SetGear(-1);
            bool isSetSpeed = car.SetSpeed(10);
            Assert.True(isSetGear);
            Assert.True(isSetSpeed);
            Assert.Equal(-1, car.GetGear());
            Assert.Equal(Car.Direction.back, car.GetDirection());
            Assert.Equal(10, car.GetSpeed());
        }

        [Fact]
        public void Gear_not_when_not_engine()
        {
            Car car = new Car();
            bool isSeGear = car.SetGear(1);
            Assert.False(isSeGear);
            Assert.False(car.IsTurnedOn());
        }

        [Fact]
        public void Gear_wont_when_no_this_gear()
        {
            Car car = new Car();
            car.TurnOnEngine();
            bool isSetGear = car.SetGear(10);
            Assert.False(isSetGear);
            Assert.Equal(0, car.GetGear());
        }

        [Fact]
        public void Gear_sets_2_when_speed_limit_is_catched()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.SetGear(1);
            car.SetSpeed(20);
            bool isSetGear = car.SetGear(2);
            Assert.True(isSetGear);
            Assert.Equal(2, car.GetGear());
            Assert.Equal(Car.Direction.forward, car.GetDirection());
            Assert.Equal(20, car.GetSpeed());
        }

        [Fact]
        public void Gear_wont_backwards_when_gone_back_and_went_to_neutral()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.SetGear(-1);
            car.SetSpeed(10);
            car.SetGear(0);
            bool isSetGear = car.SetGear(-1);
            Assert.False(isSetGear);
            Assert.Equal(0, car.GetGear());
            Assert.Equal(Car.Direction.back, car.GetDirection());
        }
    }

    public class SpeedUnitTests
    {
        [Fact]
        public void Speed_wont_change_when_engine_0ff()
        {
            Car car = new Car();
            bool isSetSpeed = car.SetSpeed(10);
            Assert.False(isSetSpeed);
            Assert.False(car.IsTurnedOn());
        }

        [Fact]
        public void Speed_wont_change_when_neutral()
        {
            Car car = new Car();
            car.TurnOnEngine();
            bool isSetSpeed = car.SetSpeed(10);
            Assert.False(isSetSpeed);
            Assert.Equal(0, car.GetSpeed());
        }

        [Fact]
        public void Speed_will_change_cause_of_upper_limit_and_direction()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.SetGear(1);
            car.SetSpeed(20);
            car.SetGear(2);
            bool isSetSpeed = car.SetSpeed(50);
            Assert.True(isSetSpeed);
            Assert.Equal(2, car.GetGear());
            Assert.Equal(50, car.GetSpeed());
            Assert.Equal(Car.Direction.forward, car.GetDirection());
        }

        [Fact]
        public void Speed_will_change_cause_of_max_for_backwards_and_direction_back()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.SetGear(-1);
            bool isSetSpeed = car.SetSpeed(20); ;
            Assert.True(isSetSpeed);
            Assert.Equal(20, car.GetSpeed());
            Assert.Equal(Car.Direction.back, car.GetDirection());
            Assert.Equal(-1, car.GetGear());
        }

        [Fact]
        public void Speed_wont_change_when_minus()
        {
            Car car = new Car();
            car.TurnOnEngine();
            bool isSetSpeed = car.SetSpeed(-30);
            Assert.False(isSetSpeed);
            Assert.Equal(0, car.GetSpeed());
        }

        [Fact]
        public void Speed_wont_change_cause_of_limit_of_gear()
        {
            Car car = new Car();
            car.TurnOnEngine();
            car.SetGear(1);
            car.SetSpeed(30);
            car.SetGear(4);
            car.SetSpeed(50);
            car.SetGear(5);
            bool isSetSpeed = car.SetSpeed(130); ;
            Assert.False(isSetSpeed);
            Assert.Equal(50, car.GetSpeed());
            Assert.Equal(Car.Direction.forward, car.GetDirection());
            Assert.Equal(5, car.GetGear());
        }
    }
}