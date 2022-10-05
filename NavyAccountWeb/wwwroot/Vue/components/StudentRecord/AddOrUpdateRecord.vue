<template>
    <div>
        <div v-if="errors" class="has-error"> {{ [errors] }}</div>
        <div v-if="responseMessage" class="has-error"> {{ responseMessage }}</div>
        <form @submit="checkForm"  method="post">
                <div class="sw-container tab-content">
                    <div id="validation-step-1" class="tab-pane step-content col-12" style="display: block;">
                        <div class="form-row">
                            <div class="form-group col-md-4">
                                <label>Reg Number</label>
                                <input class="form-control" name="Reg_Number" v-model="postBody.Reg_Number" :disabled="studenttoeditid > 0" placeholder=""  required />
                            </div>
                        </div>
                            <div class="form-row">
                            <div class="form-group col-md-4">
                                <label>SurName</label>
                                <input class="form-control" name="Surname" v-model="postBody.Surname" placeholder="" required />
                            </div>
                            <div class="form-group col-md-4">
                                <label>FirstName</label>
                                <input class="form-control" name="FirstName" v-model="postBody.FirstName" required />
                            </div>
                            <div class="form-group col-md-4">
                                <label>MiddleName</label>
                                <input class="form-control" name="MiddleName" v-model="postBody.MiddleName" required />
                            </div>
                            
                            </div>
                        
                        <div class="form-row">
                            <div class="col-sm-4">
                                <label>Age</label>
                                <input class="form-control" name="Age" v-model="postBody.Age" required />
                            </div>
                            <!-- <div class="col-12 col-xl-4">
                                <div class="form-group">
                                    <label class="form-label">Parent Name</label>
                                    <vuejsAutocomplete source="/api/ParentRecord/getAllParentByNameLimited/"
                                                    input-class="form-control"
                                                    @selected="setValueParentID"
                                                    name="parentid"
                                                    v-model="postBody.Parentid">
                                    </vuejsAutocomplete>
 
                                </div>
                            </div> -->
                                <div class="form-group col-md-4">
                               <label class="form-label">Parent Name</label>
                                <select class="form-control" v-model="postBody.Parentid" name="parentid" required>
                                    <option v-for="pr in parentList" v-bind:value="pr.id" v-bind:key="pr.id"> {{ pr.surname }} </option>
                                </select>
                                </div>
                               
                                <div class="form-group col-md-4">
                               <label class="form-label">Guardian Name</label>
                                <select class="form-control" v-model="postBody.Guardianid" name="guardianid" required>
                                    <option v-for="gl in guardianList" v-bind:value="gl.id" v-bind:key="gl.id"> {{ gl.surname }} </option>
                                </select>
                            </div>
                                <!-- <div class="col-12 col-xl-4"> 
                                <div class="form-group">
                                    <label class="form-label">Guardian Name</label>
                                    <vuejsAutocomplete source="/api/ParentRecord/getAllGuardianByNameLimited/"
                                                    input-class="form-control"
                                                    @selected="setValueGuardianID"
                                                    name="guardianid"
                                                    v-model="postBody.Guardianid">
                                    </vuejsAutocomplete>

                                </div>
                             </div>-->
                               

                        </div>

                        <div class="form-row">
                            <div class="form-group col-md-4">
                                <label>Sex</label>
                                <select class="form-control" v-model="postBody.Sex" name="sex" required>
                                    <option v-for="ge in gender" v-bind:value="ge.value" v-bind:key="ge.value"> {{ ge.text }} </option>
                                </select>
                            </div>
                            <div class="col-sm-4">
                                <label>Email</label>
                                <input class="form-control" name="Email" v-model="postBody.Email" />
                            </div>
                            <div class="col-md-4">
                                <label>PhoneNumber</label>
                                <input class="form-control" name="PhoneNumber" v-model="postBody.PhoneNumber" />
                            </div>
                        </div>

                        <div class="form-row">
                            
                            <div class="form-group col-md-4">
                                <label>Class</label>
                                <select class="form-control" v-model="postBody.ClassId" name="classId" required>
                                    <option v-for="cls in classList" v-bind:value="cls.id" v-bind:key="cls.id"> {{ cls.className }} </option>
                                </select>
                            </div>
                            <div class="col-sm-4">
                                <label>School</label>
                                <select class="form-control" v-model="postBody.SchoolId" name="schoolId" required>
                                    <option v-for="sch in schoolList" v-bind:value="sch.id" v-bind:key="sch.id"> {{ sch.schoolname }} </option>
                                </select>
                            </div>
                            <div class="form-group col-md-4">
                                <label>Class Category</label>
                                <input class="form-control" name="ClassCategory" v-model="postBody.ClassCategory" />
                            </div>
                            

                        </div>
                        <div class="form-row">
                            <div class="form-group col-md-4">
                                <label>Status</label>
                                <select class="form-control" v-model="postBody.Status" name="status" required>
                                    <option v-for="stu in statusList" v-bind:value="stu.value" v-bind:key="stu.value"> {{ stu.text }} </option>
                                </select>
                            </div>
                            <div class="col-md-4">
                                <label>Enrolment Date</label>
                                <vuejsDatepicker v-model="postBody.CommencementDate" input-class="form-control" name="commencementDate"></vuejsDatepicker>
                            </div>

                             <div class="col-md-4">
                                <label>Exit Left</label>
                                <vuejsDatepicker v-model="postBody.ExitDate" input-class="form-control" name="cxitDate"></vuejsDatepicker>
                            </div>
                            <div class="col-md-4">
                                <label>Exit Reason</label>
                                <input class="form-control" name="exitReason" v-model="postBody.ExitReason" />
                            </div>
                            <!-- <div class="filter">
                             <label for="autocomplete-dropdown">Autocomplete dropdown: </label>
                             <autocomplete-dropdown id="autocomplete-dropdown" :options="fruitOptions" v-model="postBody.ExitReason"></autocomplete-dropdown>
                            </div> -->
                        </div>
                      
                        <div class="row">
                    <div class="col-12 ">
                        <div class="btn-group mr-2 sw-btn-group-extra" v-if="canProcess" role="group">
                            <button class="btn btn-submit btn-primary" v-on:click="checkForm"   type="submit">{{submitorUpdate}}</button>
                        </div>
                    </div>
                   </div>
                    </div>

                </div>
        </form>
    </div>
</template>

<script>
import Axios from 'axios';
import vuejsAutocomplete from 'vuejs-auto-complete'
import vuejsDatepicker from 'vuejs-datepicker'
export default {
    props:['studenttoeditid'],
    components: {
            vuejsDatepicker,
            vuejsAutocomplete,
            Axios
        },
    data() {
        return{
            errors:null,
            responseMessage:'',
            submitorUpdate:'Submit',
            canProcess: true,
            stateList:null,
            parentList: null,
            guardianList: null,
            classList: null,
            schoolList: null,
            postBody:{
                Reg_Number:'',
                Surname:'',
                FirstName:'',
                MiddleName:'',
                Sex:'',
                SchoolCode:'',
                CommencementDate:'',
                Class:'',
                PhoneNumber:'',
                Email:'',
                Status:'',
                ExitDate:'',
                ExitReason:'',
                Age:'',
                ClassCategory:'',
                ParentalStatus:'',
                Parentid:null,
                Guardianid:null,
                studentid:null,
                ClassId:null,
                SchoolId:null
            },
            gender: [
                    { value: 'M', text: 'Male' },
                    { value: 'F', text: 'Female' }
                ],
                statusList: [
                    { value: 'Active', text: 'Active' },
                    { value: 'On Claim', text: 'On Claim' },
                    { value: 'Inactive', text: 'Inactive' }
                ],
                exitreasonList: [
                    { value: 'Graduated', text: 'Graduated' },
                    { value: 'Suspended', text: 'Suspended' },
                    { value: 'Expelled', text: 'Expelled' },
                    { value: 'Death', text: 'Death' },
                    { value: 'Absconded', text: 'Absconded' }

                ]

        };
        
    },
    mounted(){
        Axios
        .get('/api/ParentRecord/GetAll')
        .then(response => (this.parentList = response.data));
        Axios
        .get('/api/GuardianRecord/GetAll')
        .then(response => (this.guardianList = response.data));
        Axios
        .get('/api/SchoolRecord/GetAll')
        .then(response => (this.schoolList = response.data));
        Axios
        .get('/api/statictable/getallclass')
        .then(response => (this.classList = response.data));

        if (this.studenttoeditid != 0) {
                alert('i am here')
                Axios
                    .get(`/api/StudentRecord/GetStudentById2/${this.studenttoeditid}`)
                    .then(response => {
                        alert(response.data.reg_Number);
                        this.postBody.Reg_Number= response.data.reg_Number
                        this.postBody.Surname= response.data.surname
                        this.postBody.FirstName= response.data.firstName
                        this.postBody.MiddleName= response.data.middleName
                        this.postBody.Sex= response.data.sex
                        this.postBody.SchoolCode= response.data.schoolCode
                        this.postBody.CommencementDate= response.data.commencementDate
                        this.postBody.Class= response.data.class
                        this.postBody.ClassId= response.data.classId
                        this.postBody.SchoolId= response.data.schoolId
                        this.postBody.Parentid= response.data.parentid
                        this.postBody.Guardianid= response.data.guardianid
                        this.postBody.PhoneNumber= response.data.phoneNumber
                        this.postBody.Email= response.data.email
                        this.postBody.Status= response.data.status
                        this.postBody.ExitDate= response.data.exitDate
                        this.postBody.ExitReason= response.data.exitReason
                        this.postBody.Age= response.data.age
                        this.postBody.ClassCategory= response.data.classCategory
                        this.postBody.ParentalStatus= response.data.parentalStatus
                        this.submitorUpdate='update'              
                    })

            }
    },
    methods:{
        setValueParentID(result){
            this.postBody.Parentid=result.value;
            },
            setValueGuardianID(result){
            this.postBody.Guardianid=result.value;
            },
            
        checkForm: function (e) {
            alert(this.postBody.Reg_Number);
            if (this.postBody.Reg_Number) {
                 e.preventDefault();
                 this.canProcess = false;
                 this.postData();
             }
             else{
   
             this.errors = [];
               this.errors.push('Code is Required');
             }
           },
        postData(){
            if(this.submitorUpdate=='Submit'){
            Axios.post(`/api/StudentRecord/CreateStudent`,this.postBody)
            .then(response=>{
                this.responseMessage=response.data.responseDescription;
                this.canProcess = true;
                if(response.data.responseCode=='200'){
                    this.postBody = {
                                    Reg_Number:'',Surname:'', FirstName:'',                                
                                    MiddleName:'', Sex:'', SchoolCode:'',
                                    CommencementDate:'',Class:'',  PhoneNumber:'',
                                    Email:'',Status:'',ExitDate:'', ExitReason:'',
                                    Age:'',ClassCategory:'',ParentalStatus:''
                            };
                }
            }).catch(e=>{
                this.errors.push(e);
            })
            }
            if(this.submitorUpdate=='update'){
                alert('i am here 444');
                Axios.put(`/api/StudentRecord/Update`,this.postBody)
                .then(response=>{
                this.responseMessage=response.data.responseDescription;
                if(response.data.responseCode=='200'){
                    this.submitorUpdate='Submit';
                    this.postBody = {
                                    Reg_Number:'',Surname:'', FirstName:'',                                
                                    MiddleName:'', Sex:'', SchoolCode:'',
                                    CommencementDate:'',Class:'',  PhoneNumber:'',
                                    Email:'',Status:'',ExitDate:'', ExitReason:'',
                                    Age:'',ClassCategory:'',ParentalStatus:''
                            };
                }
            }).catch(e=>{
                this.errors.push(e);
            })
            }
        }
       
    },
    computed:{

    }
}
</script>
