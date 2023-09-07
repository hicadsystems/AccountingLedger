<template>
    <!-- WRAPPER -->
<div>
    <div class="card-body">
        <div class="row">
            <div class="col-sm-4" v-if="schcheck">
                                <label>School</label>
                                <select class="form-control" v-model="SchoolId" name="schoolId" @change="getstudents(SchoolId)"  required>
                                    <option v-for="sch in schoolList" v-bind:value="sch.id" v-bind:key="sch.id"> {{ sch.schoolname }} </option>
                                </select>

                            </div>  
             <div class="col-12 col-xl-4" v-if="SchoolId">
                <div class="form-group">
                    <label class="form-label">Student Number</label>
                    <vuejsAutocomplete source="/api/StudentRecord/getAllStudentByNameLimited/"
                                       input-class="form-control"
                                       @selected="setValueStudent"
                                       v-model="pp">
                    </vuejsAutocomplete>

                </div>
            </div>
        </div>
    <div v-if="SchoolId">
        <table id="datatables-buttons" class="table table-striped" style="width:100%">
            <thead>
                <tr>
                    <th>Registration Number</th>
                    <th>Full Name</th>
                    <th>Parent Name </th>
                    <th>Gender</th>
                    <th>Age</th>
                    <th>Class</th>
                    <th>School</th>
                    <th>Update</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="student in studentList">
                    <td>{{ student.reg_Number }}</td>
                    <td>{{ student.surname}}  {{ student.firstName }}  {{ student.middleName }}</td>
                    <td>{{ student.parentStatus }}</td>
                    <td>{{ getAppropriateGender(student.sex) }}</td>
                    <td>{{ student.age }}</td>
                    <td>{{ student.className }}</td>
                    <td>{{ student.schoolCode }}</td>
                   <td><a type="button" :href="'Create?id='+student.id" class="btn btn-submit btn-primary">Edit</a></td>
                </tr>
            </tbody>

        </table>
        <paginate
           
            :page-count="getPageCount"
            :page-range="3"
            :margin-pages="2"
            :click-handler="clickCallback"
            :prev-text="'Prev'"
            :next-text="'Next'"
            class="pagination"
            :container-class="'pagination'"
            :page-class="'page-item'">
        </paginate>
    </div>
    </div>
    

</div>

    <!-- END WRAPPER -->
</template>

<script>
import Paginate from 'vuejs-paginate'
import vuejsAutocomplete from 'vuejs-auto-complete'
export default {
components:{
    Paginate,
     vuejsAutocomplete
},
data() {
    return {
    studentList:null,
    schoolList:null,
    schcheck:true,
    pageno:0,
    totalcount:0,
      id:0,
      SchoolId:null,
     pp:''
     
    };
},
 created() {
        this.$store.state.objectToUpdate = null;
},
computed: {
    getPageCount :function() {
        return ( Math.ceil(this.totalcount/10) - 0);
    }
},
mounted () {
    axios
        .get('/api/SchoolRecord/GetAll')
        .then(response => (this.schoolList = response.data));
    axios
    .get(`/api/StudentRecord/getAllStudents/${this.SchoolId}?pageno=${this.pageno}`)
    .then(response => {
        this.studentList =  response.data.studentlist;
        this.totalcount = response.data.total;
        this.schcheck=false;
    })
 },
   
 methods: {
    getstudents:function(SchoolId){
        if(SchoolId>0){
        axios
        .get(`/api/StudentRecord/getAllStudents/${SchoolId}?pageno=${this.pageno}`)
        .then(response => {
            this.studentList =  response.data.studentlist;
            this.totalcount = response.data.total;
    })
    }
    },
     clickCallback: function (pageNum) {
         this.pageno = pageNum;
        //  alert(this.SchoolId)
         axios.get(`/api/StudentRecord/getAllStudents/${this.SchoolId}?pageno=${this.pageno}`)
        .then(response => {
            this.studentList = response.data.studentlist;
            this.totalcount = response.data.total;
        })
     },
      setValueStudent: function(result) {
        //   alert(result.value)
         axios
       .get(`/api/StudentRecord/getStudentByID/${result.value}`)
       .then(response => {this.studentList = response.data;
      
       })
    },

     processRetrieve: function (mainAccount) {

        //this.$store.state.objectToUpdate = mainAccount;
     },

     getAppropriateGender: function (gender) {
         if (gender == 'M')
             return 'Male';
         if (gender == 'F')
             return 'Female';
     }
}

};
</script>
<style lang="css">

.pagination{display:inline-block;padding-left:0;margin:20px 0;border-radius:4px}.pagination>li{display:inline}.pagination>li>a,.pagination>li>span{position:relative;float:left;padding:6px 12px;margin-left:-1px;line-height:1.42857143;color:#337ab7;text-decoration:none;background-color:#fff;border:1px solid #ddd}.pagination>li>a:focus,.pagination>li>a:hover,.pagination>li>span:focus,.pagination>li>span:hover{z-index:2;color:#23527c;background-color:#eee;border-color:#ddd}.pagination>li:first-child>a,.pagination>li:first-child>span{margin-left:0;border-top-left-radius:4px;border-bottom-left-radius:4px}.pagination>li:last-child>a,.pagination>li:last-child>span{border-top-right-radius:4px;border-bottom-right-radius:4px}.pagination>.active>a,.pagination>.active>a:focus,.pagination>.active>a:hover,.pagination>.active>span,.pagination>.active>span:focus,.pagination>.active>span:hover{z-index:3;color:#fff;cursor:default;background-color:#337ab7;border-color:#337ab7}.pagination>.disabled>a,.pagination>.disabled>a:focus,.pagination>.disabled>a:hover,.pagination>.disabled>span,.pagination>.disabled>span:focus,.pagination>.disabled>span:hover{color:#777;cursor:not-allowed;background-color:#fff;border-color:#ddd}.pagination-lg>li>a,.pagination-lg>li>span{padding:10px 16px;font-size:18px;line-height:1.3333333}.pagination-lg>li:first-child>a,.pagination-lg>li:first-child>span{border-top-left-radius:6px;border-bottom-left-radius:6px}.pagination-lg>li:last-child>a,.pagination-lg>li:last-child>span{border-top-right-radius:6px;border-bottom-right-radius:6px}.pagination-sm>li>a,.pagination-sm>li>span{padding:5px 10px;font-size:12px;line-height:1.5}.pagination-sm>li:first-child>a,.pagination-sm>li:first-child>span{border-top-left-radius:3px;border-bottom-left-radius:3px}.pagination-sm>li:last-child>a,.pagination-sm>li:last-child>span{border-top-right-radius:3px;border-bottom-right-radius:3px}.pager{padding-left:0;margin:20px 0;text-align:center;list-style:none}.pager li{display:inline}.pager li>a,.pager li>span{display:inline-block;padding:5px 14px;background-color:#fff;border:1px solid #ddd;border-radius:15px}.pager li>a:focus,.pager li>a:hover{text-decoration:none;background-color:#eee}.pager .next>a,.pager .next>span{float:right}.pager .previous>a,.pager .previous>span{float:left}.pager .disabled>a,.pager .disabled>a:focus,.pager .disabled>a:hover,.pager .disabled>span{color:#777;cursor:not-allowed;background-color:#fff}

</style> 